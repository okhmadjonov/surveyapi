﻿using System.Security.Cryptography;
using System.Text;

namespace surveyapi.Extentions;

public static class StringExtentions
{
    public static string Encrypt(this string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            var hashedBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
            var hashedPassword = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            return hashedPassword;
        }
    }
}