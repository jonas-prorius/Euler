using System.Collections.Generic;
using System.Linq;

namespace EulerHelper
{
    public static class Miscellaneous
    {
        public static bool IsPalindrome(string s)
        {
            string z = new(s.Reverse().ToArray());
            return s == z;
        }

        public static long NextCollatz(long current)
        {
            if (current % 2 == 0)
                return current / 2;
            else return 3 * current + 1;
        }

        public static List<long> CreateLongList(long from, long to, long interval = 1)
        {
            List<long> result = new();
            for (long i = from; i <= to; i += interval)
                result.Add(i);

            return result;
        }
    }
}
