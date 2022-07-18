using System.Linq;

namespace EulerMath
{
    internal static class Divisor
    {
        internal static bool IsDivisibleBy(long number, long divisor)
        {
            return number % divisor == 0;
        }

        internal static bool IsDivisibleBy(long number, long[] divisors)
        {
            return divisors.All(divisor => IsDivisibleBy(number, divisor));
        }
    }
}
