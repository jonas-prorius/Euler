using System.Collections.Generic;
using System.Linq;

namespace EulerHelper
{
    /// <summary>
    /// Miscellaneous utility functions for general-purpose operations.
    /// </summary>
    public static class Miscellaneous
    {
        /// <summary>
        /// Determines if a string is a palindrome (reads the same forwards and backwards).
        /// </summary>
        /// <param name="s">The string to check</param>
        /// <returns>True if the string is a palindrome, false otherwise</returns>
        /// <example>
        /// <code>
        /// bool isPalindrome = Miscellaneous.IsPalindrome("12321"); // returns true
        /// bool isPalindrome2 = Miscellaneous.IsPalindrome("12345"); // returns false
        /// </code>
        /// </example>
        public static bool IsPalindrome(string s)
        {
            string z = new(s.Reverse().ToArray());
            return s == z;
        }

        /// <summary>
        /// Calculates the next number in the Collatz sequence (3n+1 problem).
        /// If the current number is even, divide by 2. If odd, multiply by 3 and add 1.
        /// </summary>
        /// <param name="current">The current number in the sequence</param>
        /// <returns>The next number in the Collatz sequence</returns>
        /// <example>
        /// <code>
        /// long next1 = Miscellaneous.NextCollatz(5); // returns 16 (3*5+1)
        /// long next2 = Miscellaneous.NextCollatz(4); // returns 2 (4/2)
        /// </code>
        /// </example>
        public static long NextCollatz(long current)
        {
            if (current % 2 == 0)
                return current / 2;
            else return 3 * current + 1;
        }

        /// <summary>
        /// Creates a list of consecutive numbers within a specified range.
        /// </summary>
        /// <param name="from">The starting number (inclusive)</param>
        /// <param name="to">The ending number (inclusive)</param>
        /// <param name="interval">The step size between numbers (default is 1)</param>
        /// <returns>A list containing the sequence of numbers</returns>
        /// <example>
        /// <code>
        /// // Create list [1, 2, 3, 4, 5]
        /// var list = Miscellaneous.CreateLongList(1, 5);
        /// 
        /// // Create list [2, 4, 6, 8, 10]
        /// var evenList = Miscellaneous.CreateLongList(2, 10, 2);
        /// </code>
        /// </example>
        public static List<long> CreateLongList(long from, long to, long interval = 1)
        {
            List<long> result = new();
            for (long i = from; i <= to; i += interval)
                result.Add(i);

            return result;
        }
    }
}
