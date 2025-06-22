using System.Collections.Generic;

namespace EulerMath
{
    /// <summary>
    /// Extension methods for mathematical operations on collections.
    /// </summary>
    public static class CollectionProperties
    {
        /// <summary>
        /// Calculates the product of all elements in the collection.
        /// </summary>
        /// <param name="factors">The collection of numbers to multiply</param>
        /// <returns>The product of all elements</returns>
        /// <example>
        /// <code>
        /// var numbers = new List&lt;long&gt; { 2, 3, 4 };
        /// long result = numbers.Product(); // returns 24
        /// </code>
        /// </example>
        public static long Product(this IEnumerable<long> factors)
        {
            long product = 1;
            foreach (long factor in factors)
                product *= factor;
            return product;
        }
    }
}
