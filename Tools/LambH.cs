
namespace Tools
{
    public static class LambH
    {
        public static bool IsBetween(this DateTime val, DateTime start, DateTime end)
        {
            return val >= start && val <= end;
        }
        public static bool IsBetween(this long val, long start, long end)
        {
            return val >= start && val <= end;
        }
        public static bool IsBetween(this int val, int start, int end)
        {
            return val >= start && val <= end;
        }
    }
}
