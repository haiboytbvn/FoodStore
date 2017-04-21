using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eating2.AppConfig
{
    public static class StringExtensions
    {
        public static int ToInt(this string input, int defaultValue)
        {
            int output;
            if (string.IsNullOrEmpty(input))
            {
                return defaultValue;

            }
            if (int.TryParse(input, out output))
            {
                return output;
            }
            return defaultValue;
        }
    }
}