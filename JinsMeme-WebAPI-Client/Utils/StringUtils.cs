using System;
using System.Linq;

namespace JinsMeme_WebAPI_Client.Utils
{
    public static class StringUtils
    {
        private static Random random = new Random();

        /**
         * Generate random string
         */

        public static string RandomAlphabetNumbersString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
