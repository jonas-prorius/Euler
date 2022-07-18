using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ProblemSolver.Helpers
{
    internal static class Stringifyer
    {
        internal static string[] Stringify(this IEnumerable<object> items)
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
            => items.Select(o => o.ToString()).ToArray();

#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        internal static string[] Stringify(this IEnumerable<int> items)
            => items.Select(o => o.ToString()).ToArray();

        internal static string[] Stringify(this IEnumerable<long> items)
            => items.Select(o => o.ToString()).ToArray();
    }
}
