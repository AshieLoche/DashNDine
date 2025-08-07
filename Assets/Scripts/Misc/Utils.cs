using System;
using UnityEngine;

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

        public static Tenum ConvertStringToEnum<Tenum, Tscript>(string str, GameObject sender)
            where Tenum : struct, Enum
            where Tscript : Component
        {
            if (!Enum.TryParse(str, true, out Tenum enumValue))
            {
                Debug.LogError($"String to {typeof(Tenum).Name} parsing failed in {typeof(Tscript).Name} at {sender.name}");
                return default;
            }

            return enumValue;
        }
    }
}