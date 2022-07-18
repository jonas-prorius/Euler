﻿using System.Collections.Generic;

namespace EulerMath
{
    public static class CollectionProperties
    {
        public static long Product(this IEnumerable<long> factors)
        {
            long product = 1;
            foreach (var factor in factors)
                product *= factor;
            return product;
        }
    }
}
