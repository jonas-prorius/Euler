using System.Linq;

namespace EulerMath
{
    public static class IntegerProperties
    {
        public static long DigitProduct(this long integer)
        {
            return integer
                .ToString()
                .ToCharArray()
                .Select(c => long.Parse(c.ToString())).Product();
        }

        public static bool IsEven(this long integer)
        {
            return integer % 2 == 0;
        }
    }
}
