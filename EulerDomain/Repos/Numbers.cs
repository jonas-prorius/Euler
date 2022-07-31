using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDb.Entities;
using EulerMath;
using Microsoft.EntityFrameworkCore;

namespace EulerDomain.Repos
{
    public class Numbers
    {
        #region Fields

        private readonly EulerDbContext _dbContext;

        #endregion Fields

        #region Constructors

        public Numbers(EulerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Constructors

        #region Public Methods

        public void EnsureCreatedUntil(long number)
        {
            IQueryable<long>? existing = _dbContext.Numbers
                .Where(n => 0 < n.Id && n.Id <= number)
                .Select(n => n.Id);

            for (long l = 0; l <= number; l++)
                if (!existing.Contains(l))
                    _dbContext.Numbers.Add(new Number(l));

            _dbContext.SaveChanges();
        }

        public async Task EnsureCreatedUntilAsync(long id)
        {
            IQueryable<long>? existing = _dbContext.Numbers
                .Where(n => 0 < n.Id && n.Id <= id)
                .Select(n => n.Id);

            if (await existing.CountAsync() == id)
                return;

            for (long l = 1; l <= id; l++)
                if (!(await existing.ContainsAsync(l)))
                    _dbContext.Numbers.AddAsync(new Number(l));

            await _dbContext.SaveChangesAsync();
        }

        private void EnsurePrimesCalculatedUntil(long number)
        {
            EnsureCreatedUntil(number);

            _dbContext.Numbers
                    .Where(n => !n.IsPrimeNumber.HasValue && n.Id <= 1)
                    .ToList()
                    .ForEach(n => n.IsPrimeNumber = false);

            List<Number>? existingWithoutPrimeSet = _dbContext.Numbers
                .Where(n => 1 < n.Id && n.Id <= number && !n.IsPrimeNumber.HasValue)
                .ToList();

            if (existingWithoutPrimeSet.Count == number)
                return;

            foreach (var dbNumber in existingWithoutPrimeSet)
                dbNumber.IsPrimeNumber = dbNumber.Id.IsPrime();

            _dbContext.SaveChanges();
        }

        private async Task EnsurePrimesCalculatedUntilAsync(long number)
        {
            await EnsureCreatedUntilAsync(number);

            (await _dbContext.Numbers
                  .Where(n => !n.IsPrimeNumber.HasValue && n.Id <= 1)
                  .ToListAsync())
                  .ForEach(n => n.IsPrimeNumber = false);

            await _dbContext.SaveChangesAsync();

            List<Number>? existingWithoutPrimeSet = await _dbContext.Numbers
                .Where(n => 1 < n.Id && n.Id <= number && !n.IsPrimeNumber.HasValue)
                .ToListAsync();

            if (existingWithoutPrimeSet.Count == number)
                return;

            foreach (var dbNumber in existingWithoutPrimeSet)
                dbNumber.IsPrimeNumber = dbNumber.Id.IsPrime();

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<long>> GetPrimesUntilAsync(long max)
        {
            await EnsurePrimesCalculatedUntilAsync(max);

            return await _dbContext.Numbers
                .Where(n => n.IsPrimeNumber.Value && n.Id <= max)
                .Select(n => n.Id)
                .ToListAsync();
        }

        public async Task<long> GetPrimeAsync(long primeNo)
        {
            while ((await _dbContext.Numbers.CountAsync(n => n.IsPrimeNumber ?? false)) < primeNo)
            {
                await AddAsync(10000);
                await EnsurePrimesCalculatedUntilAsync(await _dbContext.Numbers.MaxAsync(n => n.Id));
            }

            var numberId = await _dbContext.Numbers
                .Where(n => n.IsPrimeNumber ?? false)
                .Select(n => n.Id)
                .OrderBy(id => id)
                .Take((int)primeNo)
                .LastAsync();

            return (await _dbContext.Numbers.FirstAsync(n => n.Id == numberId)).Id;
        }

        public List<long> GetFactors(long number)
        {
            EnsureCreatedUntil(number);

            SetFactors(number);

            return _dbContext.Numbers
                .First(n => n.Id == number)
                .Factors
                .Select(f => f.FactorNumberId)
                .ToList();
        }

        public async Task<List<long>> GetFactorsAsync(long number)
        {
            await EnsureCreatedUntilAsync(number);

            await SetFactorsAsync(number);

            return (await _dbContext.Factors
                .Where(f => f.NumberId == number)
                .Select(f => f.FactorNumberId)
                .ToListAsync());
        }

        #endregion Public Methods

        #region Private Methods

        internal void Add(long number)
        {
            _dbContext.Numbers.Add(new Number(number));
            _dbContext.SaveChanges();
        }

        private async Task AddAsync(long count)
        {
            long max = _dbContext.Numbers.Max(n => n.Id);
            for (long l = max + 1; l < max + count; l++)
                await _dbContext.Numbers.AddAsync(new Number(l));

            await _dbContext.SaveChangesAsync();
        }

        private void SetPrime(long number)
        {
            var dbNumber = _dbContext.Numbers.First(n => n.Id == number);
            if (dbNumber.IsPrimeNumber.HasValue)
                return;

            dbNumber.IsPrimeNumber = number.IsPrime();
            _dbContext.SaveChanges();
        }

        private async Task SetPrimeAsync(long number)
        {
            var dbNumber = await _dbContext.Numbers.FirstAsync(n => n.Id == number);
            if (dbNumber.IsPrimeNumber.HasValue)
                return;

            dbNumber.IsPrimeNumber = number.IsPrime();
            await _dbContext.SaveChangesAsync();
        }

        private void SetFactors(long number)
        {
            var dbNumber = _dbContext.Numbers.First(n => n.Id == number);
            if (dbNumber.HasFactors.HasValue)
                return;

            SetPrime(number);

            if (dbNumber.IsPrimeNumber ?? false)
                return;

            EnsurePrimesCalculatedUntil(dbNumber.Id);

            List<Number>? potentialFactors = _dbContext.Numbers
                .Where(n => n.IsPrimeNumber ?? true && n.Id < dbNumber.Id / 2)
                .ToList();

            foreach (var potentialFactor in potentialFactors)
                if (dbNumber.Id.IsDivisibleBy(potentialFactor.Id))
                    dbNumber.Factors.Add(new Factor(dbNumber, potentialFactor));

            _dbContext.SaveChanges();
        }

        private async Task SetFactorsAsync(long number)
        {
            var dbNumber = await _dbContext.Numbers.FirstAsync(n => n.Id == number);
            if (dbNumber.HasFactors.HasValue)
                return;

            await SetPrimeAsync(number);
            await _dbContext.SaveChangesAsync();

            if (dbNumber.IsPrimeNumber ?? false)
                return;

            await EnsurePrimesCalculatedUntilAsync(dbNumber.Id);

            List<Number>? potentialFactors = await _dbContext.Numbers
                .Where(n => n.IsPrimeNumber ?? true && n.Id < dbNumber.Id / 2)
                .ToListAsync();

            foreach (var potentialFactor in potentialFactors)
                if (dbNumber.Id.IsDivisibleBy(potentialFactor.Id))
                    dbNumber.Factors.Add(new Factor(dbNumber, potentialFactor));

            dbNumber.HasFactors = true;
            await _dbContext.SaveChangesAsync();
        }

        #endregion Private Methods
    }
}
