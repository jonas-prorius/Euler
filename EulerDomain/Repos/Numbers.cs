using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public List<long> GetRange(long until, long start = 1, long interval = 1)
        {
            List<long> numbers = new();
            for (long number = start; number <= until; number += interval)
                numbers.Add(number);

            return numbers;
        }

        public async Task<List<long>> GetPrimesUntilAsync(long max)
        {
            await EnsurePrimesCalculatedUntilAsync(max);

            return await _dbContext.Numbers
                .Where(n => n.IsPrimeNumber == true && n.Id <= max)
                .Select(n => n.Id)
                .ToListAsync();
        }

        public async Task<long> GetPrimeAsync(long primeNo)
        {
            while ((await _dbContext.Numbers.CountAsync(n => n.IsPrimeNumber == true)) < primeNo)
            {
                await AddAsync(10000);
                await EnsurePrimesCalculatedUntilAsync(await GetMaxAsync());
            }

            var numberId = await _dbContext.Numbers
                .Where(n => n.IsPrimeNumber == true)
                .Select(n => n.Id)
                .OrderBy(id => id)
                .Take((int)primeNo)
                .LastAsync();

            return (await _dbContext.Numbers.FirstAsync(n => n.Id == numberId)).Id;
        }

        private Task AddAsync(long count)
        {
            _dbContext.Numbers.AddRange(GetRange(count));
            return _dbContext.SaveChangesAsync();
        }

        public List<long> GetFactors(long number)
        {
            SetFactors(number);

            return _dbContext.Numbers
                .First(n => n.Id == number)
                .Factors
                .Select(f => f.FactorNumberId)
                .ToList();
        }

        #endregion Public Methods

        #region Private Methods

        private async Task<long> GetMaxAsync()
            => await _dbContext.Numbers.MaxAsync(n => n.Id);

        private async Task EnsureCreatedUntilAsync(long id)
        {
            IQueryable<long>? existing = _dbContext.Numbers
                .Where(n => 0 < n.Id && n.Id <= id)
                .Select(n => n.Id);

            if (await existing.CountAsync() == id)
                return;

            await _dbContext.Numbers.AddRangeAsync(GetRange(id).Except(existing).Select(n => new Number(n)));
            await _dbContext.SaveChangesAsync();
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
            {
                dbNumber.IsPrimeNumber = dbNumber.Id.IsPrime();
                if (dbNumber.IsPrimeNumber == true)
                    dbNumber.HasFactors = false;
            }

            await _dbContext.SaveChangesAsync();
        }

        private void AddNumber(long number)
        {
            if (!_dbContext.Numbers.Select(n => n.Id).Contains(number))
            {
                _dbContext.Numbers.Add(new Number(number));
                _dbContext.SaveChanges();
            }
        }

        private void AddNumbers(IEnumerable<long> numbers)
        {
            foreach (var number in numbers)
                if (!_dbContext.Numbers.Select(n => n.Id).Contains(number))
                    _dbContext.Numbers.Add(new Number(number));

            _dbContext.SaveChanges();
        }

        private void AddUntilNumber(long number)
        {
            var existing = _dbContext.Numbers
                .Where(n => 0 < n.Id && n.Id <= number)
                .Select(n => n.Id);

            for (long n = 1; n <= number; n++)
                if (existing.Contains(number))
                    _dbContext.Numbers.Add(new Number(number));

            _dbContext.SaveChanges();
        }

        private void SetPrime(long number)
        {
            var dbNumber = _dbContext.Numbers.First(n => n.Id == number);
            if (dbNumber.IsPrimeNumber.HasValue)
                return;

            dbNumber.IsPrimeNumber = number.IsPrime();
            if (dbNumber.IsPrimeNumber == true)
                dbNumber.HasFactors = false;

            _dbContext.SaveChanges();
        }

        private void SetFactors(long number)
        {
            if (!_dbContext.Numbers.Any(n => n.Id == number))
                AddNumber(number);

            if (_dbContext.Numbers.First(n => n.Id == number).HasFactors.HasValue)
                return;

            SetPrime(number);

            if (_dbContext.Numbers.First(n => n.Id == number).IsPrimeNumber == true)
                return;

            var factors = number.Factors();
            AddNumbers(factors);

            foreach (var factor in factors)
                _dbContext.Factors.Add(new Factor(number, factor));

            _dbContext.SaveChanges();
        }

        #endregion Private Methods
    }
}
