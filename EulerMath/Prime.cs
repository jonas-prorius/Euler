using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerMath
{
    public static class Prime
    {
        public static bool IsPrime(this long number, IEnumerable<long> allSmallerPrimes)
        {
            if (number < 2)
                return false;

            if (allSmallerPrimes.Where(p => p <= Math.Sqrt(number)).Any(p => number % p == 0))
                return false;

            return true;
        }

        public static bool IsPrime(this long number)
        {
            if (number <= 1)
                return false;

            if (number == 2 || number == 3)
                return true;

            if (number.IsEven())
                return false;

            for (long i = 3; i <= Math.Sqrt(number); i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
