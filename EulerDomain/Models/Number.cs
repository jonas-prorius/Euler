using System;
using System.Collections.Generic;
using System.Linq;
using EulerDb;
using EulerMath;
using DbE = EulerDb.Entities;

namespace EulerDomain.Models
{
    public class Number : DbE.Number
    {
        private readonly EulerDbContextFactory _dbFactory;

        public Number(long id, EulerDbContextFactory dbFactory) : base(id)
        {
            _dbFactory = dbFactory;
        }

        public Number(long id, bool? isPrimeNumber, EulerDbContextFactory dbFactory) : base(id, isPrimeNumber)
        {
            _dbFactory = dbFactory;
            if (isPrimeNumber.HasValue)
                IsPrimeNumber = isPrimeNumber.Value;
        }

        public Number(DbE.Number number, EulerDbContextFactory dbFactory) : base(number.Id, number.IsPrimeNumber)
        {
            _dbFactory = dbFactory;
            if (number.IsPrimeNumber.HasValue)
                IsPrimeNumber = number.IsPrimeNumber.Value;
        }

        //public new IEnumerable<Number> Factors
        //{
        //    get
        //    {
        //        if (base.Factors.Any())
        //            return base.Factors.Select(f => new Number(f, _dbFactory));

        //        if (IsPrimeNumber ?? false)
        //            return Array.Empty<Number>();

        //        using (EulerDbContext? db = _dbFactory.CreateDbContext())
        //        {
        //            List<DbE.Number>? potentialFactors = db.Numbers
        //                .Where(n => n.IsPrimeNumber ?? true && n.Id < Id / 2)
        //                .ToList();

        //            foreach (var potentialFactor in potentialFactors)
        //                if (this.IsDivisibleBy(potentialFactor.Id))
        //                    base.Factors.Add(potentialFactor);

        //            db.SaveChanges();
        //        }

        //        return base.Factors.Select(f => new Number(f, _dbFactory));
        //    }
        //}
    }

    public static class NumberExtensions
    {
        public static bool IsDivisibleBy(this Number number, long divisor)
            => number.Id % divisor == 0;

        public static bool IsEven(this Number number)
            => number.Id % 2 == 0;

        public static long DigitSum(this Number number)
            => DigitSum(number.Id);

        private static long DigitSum(long number)
            => Split(number).Sum();

        public static long MinimizedDigitSum(this Number number)
        {
            long numberSum = DigitSum(number.Id);
            while (numberSum > 10)
                numberSum = DigitSum(numberSum);

            return numberSum;
        }

        public static long DigitProduct(this Number number)
            => Split(number.Id).Product();

        private static IEnumerable<long> Split(long number)
        {
            List<long> listOfInts = new();
            while (number > 0)
            {
                listOfInts.Add(number % 10);
                number /= 10;
            }

            listOfInts.Reverse();
            return listOfInts;
        }
    }
}
