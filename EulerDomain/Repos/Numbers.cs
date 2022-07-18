using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EulerDb;
using EulerDomain.Models;
using Microsoft.EntityFrameworkCore;
using DbE = EulerDb.Entities;

namespace EulerDomain.Repos
{
    public class Numbers
    {
        private readonly EulerDbContextFactory _dbFactory;

        public Numbers(EulerDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Number Get(long id)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                return new Number(db.Numbers.First(n => n.Id == id), _dbFactory);
            }
        }

        public IList<Number> GetRange(long from, long to)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                return db.Numbers
                    .Where(n => from <= n.Id && n.Id <= to)
                    .Select(n => new Number(n, _dbFactory))
                    .ToList();
            }
        }

        public async Task<IList<Number>> GetRangeAsync(long from, long to)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                return await db.Numbers
                    .Where(n => from <= n.Id && n.Id <= to)
                    .Select(n => new Number(n, _dbFactory))
                    .ToListAsync();
            }
        }

        public void Add(Number number)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                db.Add(number);
                db.SaveChanges();
            }
        }

        public async Task AddAsync(Number number)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                await db.AddAsync(number);
                await db.SaveChangesAsync();
            }
        }

        public async Task AddAsync(long count)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                long max = db.Numbers.Max(n => n.Id);
                for (long l = max + 1; l < max + count; l++)
                    await AddAsync(new Number(l, _dbFactory));
            }
        }

        public Number Max()
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                return new Number(db.Numbers.Last(), _dbFactory);
            }
        }

        public void EnsureCreatedUntil(long id)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                IQueryable<long>? existing = db.Numbers.Select(n => n.Id);
                for (long l = 0; l <= id; l++)
                {
                    if (!existing.Contains(l))
                    {
                        db.Add(new DbE.Number(l));
                        db.SaveChanges();
                    }
                }
            }
        }

        public async Task EnsureCreatedUntilAsync(long id)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                IQueryable<long>? existing = db.Numbers.Where(n => 0 < n.Id && n.Id <= id).Select(n => n.Id);
                if (existing.Count() == id)
                    return;

                for (long l = 1; l <= id; l++)
                    if (!(await existing.ContainsAsync(l)))
                        await db.AddAsync(new DbE.Number(l));

                await db.SaveChangesAsync();
            }
        }

        public void EnsurePrimesCalculatedUntil(long id)
        {
            EnsureCreatedUntil(id);

            using (var db = _dbFactory.CreateDbContext())
            {
                IQueryable<DbE.Number>? existing = db.Numbers
                    .Where(n => 1 < n.Id && n.Id <= id && !n.IsPrimeNumber.HasValue);
                if (existing.Count() == id)
                    return;

                foreach (var number in existing)
                    number.SetPrime(existing.Where(n => n.Id < Math.Sqrt(number.Id) && (n.IsPrimeNumber ?? true)).ToList());

                db.SaveChanges();
            }
        }

        public async Task EnsurePrimesCalculatedUntilAsync(long id)
        {
            await EnsureCreatedUntilAsync(id);

            using (var db = _dbFactory.CreateDbContext())
            {
                db.Numbers
                    .Where(n => n.Id <= 1 && !n.IsPrimeNumber.HasValue)
                    .ToList()
                    .ForEach(n => n.IsPrimeNumber = false);

                await db.SaveChangesAsync();

                List<DbE.Number>? existingWithoutPrimeSet = db.Numbers
                    .Where(n => 1 < n.Id && n.Id <= id && !n.IsPrimeNumber.HasValue)
                    .ToList();

                if (existingWithoutPrimeSet.Count() == id)
                    return;

                foreach (var number in existingWithoutPrimeSet)
                {
                    number.SetPrime(db.Numbers
                        .Where(n => 1 < n.Id && n.Id <= id && (n.IsPrimeNumber ?? false))
                        .ToList());
                    await db.SaveChangesAsync();
                }
            }
        }

        public async Task<Number> GetPrimeAsync(long primeNo)
        {
            using (var db = _dbFactory.CreateDbContext())
            {
                while (db.Numbers.Count(n => n.IsPrimeNumber ?? false) < primeNo)
                {
                    await AddAsync(10000);
                    await EnsurePrimesCalculatedUntilAsync(db.Numbers.Max(n => n.Id));
                }

                var numberId = db.Numbers
                    .Where(n => n.IsPrimeNumber ?? false)
                    .Select(n => n.Id)
                    .OrderBy(id => id)
                    .Take((int)primeNo)
                    .Last();

                return new Number(db.Numbers.First(n => n.Id == numberId), _dbFactory);
            }
        }
    }
}
