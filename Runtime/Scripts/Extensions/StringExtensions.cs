using System;
using UnityEngine;

public static class StringExtensions
{
    public static string CapitalizeFirstLetter(this string str)
    {
        if (String.IsNullOrEmpty(str)) return str;
        return char.ToUpper(str[0]) + str.Substring(1);
    }
}