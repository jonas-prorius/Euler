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

        private readonly EulerDbContextFactory _dbFactory;

        #endregion Fields

        #region Constructors

        public Numbers(EulerDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        #endregion Constructors

        #region Public Methods

        public void EnsureCreatedUntil(long number)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                IQueryable<long>? existing = db.Numbers
                    .Where(n => 0 < n.Id && n.Id <= number)
                    .Select(n => n.Id);

                for (long l = 0; l <= number; l++)
                    if (!existing.Contains(l))
                        db.Numbers.Add(new Number(l));

                db.SaveChanges();
            }
        }

        public async Task EnsureCreatedUntilAsync(long id)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                IQueryable<long>? existing = db.Numbers
                    .Where(n => 0 < n.Id && n.Id <= id)
                    .Select(n => n.Id);

                if (await existing.CountAsync() == id)
                    return;

                for (long l = 1; l <= id; l++)
                    if (!(await existing.ContainsAsync(l)))
                        await db.Numbers.AddAsync(new Number(l));

                await db.SaveChangesAsync();
            }
        }

        private void EnsurePrimesCalculatedUntil(long number)
        {
            EnsureCreatedUntil(number);

            using (var db = _dbFactory.CreateDbContext())
            {
                db.Numbers
                    .Where(n => !n.IsPrimeNumber.HasValue && n.Id <= 1)
                    .ToList()
                    .ForEach(n => n.IsPrimeNumber = false);

                List<Number>? existingWithoutPrimeSet = db.Numbers
                    .Where(n => 1 < n.Id && n.Id <= number && !n.IsPrimeNumber.HasValue)
                    .ToList();

                if (existingWithoutPrimeSet.Count == number)
                    return;

                foreach (var dbNumber in existingWithoutPrimeSet)
                    dbNumber.IsPrimeNumber = dbNumber.Id.IsPrime();

                db.SaveChanges();
            }
        }

        private async Task EnsurePrimesCalculatedUntilAsync(long number)
        {
            await EnsureCreatedUntilAsync(number);

            using (var db = _dbFactory.CreateDbContext())
            {
                (await db.Numbers
                      .Where(n => !n.IsPrimeNumber.HasValue && n.Id <= 1)
                      .ToListAsync())
                      .ForEach(n => n.IsPrimeNumber = false);

                await db.SaveChangesAsync();

                List<Number>? existingWithoutPrimeSet = await db.Numbers
                    .Where(n => 1 < n.Id && n.Id <= number && !n.IsPrimeNumber.HasValue)
                    .ToListAsync();

                if (existingWithoutPrimeSet.Count == number)
                    return;

                foreach (var dbNumber in existingWithoutPrimeSet)
                    dbNumber.IsPrimeNumber = dbNumber.Id.IsPrime();

                await db.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<long>> GetPrimesUntilAsync(long max)
        {
            await EnsurePrimesCalculatedUntilAsync(max);
            using (var db = _dbFactory.CreateDbContext())
            {
                return await db.Numbers
                    .Where(n => n.IsPrimeNumber.Value && n.Id <= max)
                    .Select(n => n.Id)
                    .ToListAsync();
            }
        }

        public async Task<long> GetPrimeAsync(long primeNo)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                while ((await db.Numbers.CountAsync(n => n.IsPrimeNumber ?? false)) < primeNo)
                {
                    await AddAsync(10000);
                    await EnsurePrimesCalculatedUntilAsync(await db.Numbers.MaxAsync(n => n.Id));
                }

                var numberId = await db.Numbers
                    .Where(n => n.IsPrimeNumber ?? false)
                    .Select(n => n.Id)
                    .OrderBy(id => id)
                    .Take((int)primeNo)
                    .LastAsync();

                return (await db.Numbers.FirstAsync(n => n.Id == numberId)).Id;
            }
        }

        public IEnumerable<long> GetFactors(long number)
        {
            EnsureCreatedUntil(number);

            using (var db = _dbFactory.CreateDbContext())
            {
                SetFactors(number);

                return db.Numbers
                    .First(n => n.Id == number)
                    .Factors
                    .Select(f => f.FactorNumberId);
            }
        }

        public async Task<IEnumerable<long>> GetFactorsAsync(long number)
        {
            await EnsureCreatedUntilAsync(number);

            await SetFactorsAsync(number);

            using (var db = _dbFactory.CreateDbContext())
            {
                return (await db.Factors
                    .Where(f => f.NumberId == number)
                    .Select(f => f.FactorNumberId)
                    .ToListAsync());
            }
        }

        #endregion Public Methods

        #region Private Methods

        internal void Add(long number)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                db.Numbers.Add(new Number(number));
                db.SaveChanges();
            }
        }

        private async Task AddAsync(long count)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                long max = db.Numbers.Max(n => n.Id);
                for (long l = max + 1; l < max + count; l++)
                    await db.Numbers.AddAsync(new Number(l));

                await db.SaveChangesAsync();
            }
        }

        private long Max()
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                return db.Numbers.OrderBy(n => n.Id).Last().Id;
            }
        }

        private void SetPrime(long number, IEnumerable<long> allSmallerPrimes)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                var dbNumber = db.Numbers.First(n => n.Id == number);
                if (dbNumber.IsPrimeNumber.HasValue)
                    return;

                if (allSmallerPrimes != null)
                    dbNumber.IsPrimeNumber = number.IsPrime(allSmallerPrimes);

                db.SaveChanges();
            }
        }

        private void SetPrime(long number)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                var dbNumber = db.Numbers.First(n => n.Id == number);
                if (dbNumber.IsPrimeNumber.HasValue)
                    return;

                dbNumber.IsPrimeNumber = number.IsPrime();
                db.SaveChanges();
            }
        }

        private async Task SetPrimeAsync(long number)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                var dbNumber = await db.Numbers.FirstAsync(n => n.Id == number);
                if (dbNumber.IsPrimeNumber.HasValue)
                    return;

                dbNumber.IsPrimeNumber = number.IsPrime();
                await db.SaveChangesAsync();
            }
        }

        private void SetFactors(long number)
        {
            using (EulerDbContext? db = _dbFactory.CreateDbContext())
            {
                var dbNumber = db.Numbers.First(n => n.Id == number);
                if (dbNumber.HasFactors.HasValue)
                    return;

                SetPrime(number);

                if (dbNumber.IsPrimeNumber ?? false)
                    return;

                EnsurePrimesCalculatedUntil(dbNumber.Id);

                List<Number>? potentialFactors = db.Numbers
                    .Where(n => n.IsPrimeNumber ?? true && n.Id < dbNumber.Id / 2)
                    .ToList();

                foreach (var potentialFactor in potentialFactors)
                    if (dbNumber.Id.IsDivisibleBy(potentialFactor.Id))
                        dbNumber.Factors.Add(new Factor(dbNumber, potentialFactor));

                db.SaveChanges();
            }
        }

        private async Task SetFactorsAsync(long number)
        {
            using (EulerDbContext? db = _dbFactory.CreateDbContext())
            {
                var dbNumber = await db.Numbers.FirstAsync(n => n.Id == number);
                if (dbNumber.HasFactors.HasValue)
                    return;

                await SetPrimeAsync(number);
                await db.SaveChangesAsync();

                if (dbNumber.IsPrimeNumber ?? false)
                    return;

                await EnsurePrimesCalculatedUntilAsync(dbNumber.Id);

                List<Number>? potentialFactors = await db.Numbers
                    .Where(n => n.IsPrimeNumber ?? true && n.Id < dbNumber.Id / 2)
                    .ToListAsync();

                foreach (var potentialFactor in potentialFactors)
                    if (dbNumber.Id.IsDivisibleBy(potentialFactor.Id))
                        dbNumber.Factors.Add(new Factor(dbNumber, potentialFactor));

                dbNumber.HasFactors = true;
                await db.SaveChangesAsync();
            }
        }

        #endregion Private Methods
    }
}
