using System;
using System.Linq;

namespace Common.Extentions
{
    public static class StringExtensions
    {
        public static string GetValueOrDefault(this string value, string defaultValue)
        {
            return value.IsNullOrWhiteSpace() ? defaultValue : value;
        }

        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string EnsureStartsWith(this string str, string c)
        {
            return EnsureStartsWith(str, c, StringComparison.Ordinal);
        }

        public static string EnsureStartsWith(this string str, string c, StringComparison comparisonType)
        {
            if (str == null)
            {
                throw new ArgumentNullException(nameof(str));
            }

            if (str.StartsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return c + str;
        }

        public static string RandomNumericString(int length)
        {
            var random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
