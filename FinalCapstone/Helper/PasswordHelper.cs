﻿using System;
using System.Text;
using FinalCapstone.Models;

namespace FinalCapstone.Helper
{
    public class PasswordHelper
    {
        public PasswordHelper() { }

        public string GenerateSHA256Hash(string password, Librarian librarian)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(password + librarian.Salt);
            var sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);

            return ByteArrayToHexString(hash);
        }

        public string CreateSalt()
        {
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[10];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }

        public static string ByteArrayToHexString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public Boolean ValidateHash(LoginViewModel logIn, Librarian librarian)
        {
            bool match = false;

            byte[] bytes = Encoding.UTF8.GetBytes(logIn.Password + librarian.Salt);
            var sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            var hashedPassword = ByteArrayToHexString(hash);

            if (hashedPassword == librarian.Password)
            {
                match = true;
            }

            return match;
        }
    }
}