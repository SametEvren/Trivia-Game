using System.Globalization;

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

        public static int ToAnswerIndex(this string answerName)
        {
            return answerName.ToUpper() switch
            {
                "A" => 0,
                "B" => 1,
                "C" => 2,
                "D" => 3,
                _ => -1
            };
        }
    }
}