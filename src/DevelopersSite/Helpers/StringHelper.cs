using System;

namespace DevelopersSite.Helpers
{
    public static class StringHelper
    {
        public static string KeepAfter(this string str, string term)
        {
            var index = str.IndexOf(term, StringComparison.Ordinal);
            if (index >= 0)
            {
                str = str.Remove(0, index + term.Length);
            }

            return str;
        }

        public static string KeepUntil(this string str, string term)
        {
            var index = str.IndexOf(term, StringComparison.Ordinal);
            if (index >= 0)
            {
                str = str.Remove(index, term.Length);
            }

            return str;
        }

        public static string RemoveEnding(this string str, string term)
        {
            var index = str.IndexOf(term, StringComparison.Ordinal);
            if (index >= 0)
            {
                str = str.Remove(index, term.Length);
            }

            return str;
        }
    }
}
