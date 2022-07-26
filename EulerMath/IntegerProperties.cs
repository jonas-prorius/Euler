using System.Collections.Generic;
using System.Linq;

namespace EulerMath
{
    public static class IntegerProperties
    {
        public static bool IsDivisibleBy(this long number, long divisor)
              => number % divisor == 0;

        public static bool IsEven(this long number)
            => number % 2 == 0;

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
