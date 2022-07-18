using System.Data;
using System.Linq;

namespace ProblemSolver.Helpers
{
    public static class Splitter
    {
        public static long[] ConvertToLongArray(this string[] args) => args.Select(a => long.Parse(a)).ToArray();

        public static long ConvertToLong(this string[] args) => long.Parse(args.Single());
    }
}
