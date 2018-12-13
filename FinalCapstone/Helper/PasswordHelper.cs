using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using FinalCapstone.Models;

namespace FinalCapstone.Helper
{
    public class PasswordHelper
    {
        public PasswordHelper() { }

        public string GenerateSHA256Hash(string password, User user)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + user.Salt);
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

        public Boolean ValidateHash(LogInViewModel logIn, User user)
        {
            bool match = false;

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(logIn.Password + user.Salt);
            var sha256HashString = new System.Security.Cryptography.SHA256Managed();
            byte[] hash = sha256HashString.ComputeHash(bytes);
            var hashedPassword = ByteArrayToHexString(hash);

            if (hashedPassword == user.Password)
            {
                match = true;
            }

            return match;
        }
    }
}