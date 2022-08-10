using System.Collections.Generic;
using System.Linq;

namespace EulerMath
{
    internal static class Permutator
    {
        internal static IEnumerable<IEnumerable<T>> Combine<T>(IEnumerable<T> items, bool ignoreOrder)
        {
            IEnumerable<IEnumerable<T>> result = new List<List<T>>();
            foreach (T? item in items)
            {
            }
            if (ignoreOrder)
                result = result.Select(x => x.OrderBy(t => t).Distinct());
            //if (!repeatItems)
            //    result = result.Where(x => x.Count() == x.Distinct().Count());
            return result;
        }
    }
}
