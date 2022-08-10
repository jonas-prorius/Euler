using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerMath
{
    public static class IntegerProperties
    {
        public static bool IsDivisibleBy(this long number, long divisor)
              => number % divisor == 0;

        public static bool IsDivisibleBy(long number, long[] divisors)
            => divisors.All(divisor => IsDivisibleBy(number, divisor));

        public static bool IsEven(this long number)
            => number % 2 == 0;

        public static List<long> Factors(this long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be greater than 0");

            List<long> result = new();
            if (number.IsEven())
                result.Add(2);

            for (long i = 3; i <= Math.Sqrt(number); i += 2)
                if (number.IsDivisibleBy(i))
                    result.Add(i);

            return result;
        }

        public static long? GetLargestFactor(this long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be greater than 0");

            for (long i = (long)Math.Ceiling(Math.Sqrt(number)); i > 1; i -= 2)
                if (number.IsDivisibleBy(i))
                    return i;

            return null;
        }

        public static long DigitSum(this long number)
            => Split(number).Sum();

        public static long MinimizedDigitSum(this long number)
        {
            long numberSum = DigitSum(number);
            while (numberSum > 10)
                numberSum = DigitSum(numberSum);

            return numberSum;
        }

        public static long DigitProduct(this long number)
            => Split(number).Product();

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
