using System;

namespace RenRen.Domain.Common.Extension
{
    public static class DateTimeExtension
    {
        public static DateTimeOffset ToDateTime(this long timestamp)
        {
            if (timestamp > 10000000000)
            {
                return DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            }
            else
            {
                return DateTimeOffset.FromUnixTimeSeconds(timestamp);
            }
        }

        public static long ToUnixTimeMilliseconds(this DateTime dateTime)
        {
            return new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();
        }
    }
}
