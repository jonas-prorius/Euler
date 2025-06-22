using System;
using System.Collections.Generic;
using System.Linq;

namespace EulerMath
{
    /// <summary>
    /// Static class providing prime number operations and utilities.
    /// </summary>
    public static class Prime
    {
        /// <summary>
        /// Determines if a number is prime using a pre-calculated list of smaller primes for efficiency.
        /// </summary>
        /// <param name="number">The number to check for primality</param>
        /// <param name="allSmallerPrimes">A collection of all primes smaller than the number</param>
        /// <returns>True if the number is prime, false otherwise</returns>
        public static bool IsPrime(this long number, IEnumerable<long> allSmallerPrimes)
        {
            if (number < 2)
                return false;

            if (allSmallerPrimes.Where(p => p <= Math.Sqrt(number)).Any(p => number % p == 0))
                return false;

            return true;
        }

        /// <summary>
        /// Determines if a number is prime using a standard primality test.
        /// </summary>
        /// <param name="number">The number to check for primality</param>
        /// <returns>True if the number is prime, false otherwise</returns>
        /// <example>
        /// <code>
        /// bool isPrime = 17L.IsPrime(); // returns true
        /// bool isPrime2 = 15L.IsPrime(); // returns false
        /// </code>
        /// </example>
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

        /// <summary>
        /// Finds the next prime number after the given number.
        /// </summary>
        /// <param name="after">The number after which to find the next prime</param>
        /// <returns>The next prime number</returns>
        /// <example>
        /// <code>
        /// long nextPrime = 10L.NextPrime(); // returns 11
        /// long nextPrime2 = 13L.NextPrime(); // returns 17
        /// </code>
        /// </example>
        public static long NextPrime(this long after)
        {
            if (after < 2)
                return 2;

            if (after.IsEven())
                after++;
            else
                after += 2;

            while (!after.IsPrime())
                after += 2;

            return after;
        }
    }
}
