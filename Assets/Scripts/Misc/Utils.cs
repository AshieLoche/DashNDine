using System;
using System.Text.RegularExpressions;

namespace DashNDine.MiscSystem
{
    public static class Utils
    {
        public static string NumberToName(string number)
        {
            return number switch
            {
                "1" => "One",
                "2" => "Two",
                "3" => "Three",
                "4" => "Four",
                "5" => "Five",
                "6" => "Six",
                _ => ""
            };
        }

        public static bool TryConvertStringToEnum<Tenum>(string value, out Tenum newEnum)
            where Tenum : struct, Enum
            => Enum.TryParse(value, true, out newEnum);

        public static string RemoveWhiteSpaceAndDash(string str)
            => Regex.Replace(str, @"[\s\-]", "");

        public static bool TryConvertStringToInt(string value, out int newInt)
            => int.TryParse(value, out newInt);

        public static bool TryConvertStringToFloat(string value, out float newFloat)
            => float.TryParse(value, out newFloat);
    }
}