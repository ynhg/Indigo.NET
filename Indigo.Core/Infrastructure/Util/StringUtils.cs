using System;

namespace Indigo.Infrastructure.Util
{
    public static class StringUtils
    {
        public static string ToLowerCase(string str)
        {
            if (str == null) return null;

            return str.ToLower();
        }

        public static string ToUpperCase(string str)
        {
            if (str == null) return null;

            return str.ToUpper();
        }

        public static string ToUnderlineString(string str)
        {
            throw new NotSupportedException();
        }

        public static bool Equals(string str1, string str2)
        {
            return Equals(str1, str2, false);
        }

        public static bool Equals(string str1, string str2, bool ignoreCase)
        {
            if (str1 == str2) return true;

            if (str1 == null || str2 == null) return false;

            return ignoreCase ? str1.Equals(str2, StringComparison.OrdinalIgnoreCase) : str1.Equals(str2);
        }
    }
}
