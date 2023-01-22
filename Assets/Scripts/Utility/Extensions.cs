﻿using System.Globalization;
using System.Text;

namespace Utility
{
    public static class Extensions
    {
        public static string ToCategoryName(this string categoryText)
        {
            return categoryText
                .Trim()
                .Replace("-"," ")
                .ToUpper(new CultureInfo("en-US", false));
        }
    }
}