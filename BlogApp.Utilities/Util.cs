using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BlogApp.Utilities
{
    public class Util
    {
        public static string GetHash(string str)
        {
            byte[] input = Encoding.UTF8.GetBytes(str);
            HashAlgorithm algorithm = new MD5CryptoServiceProvider();
            byte[] hashedBytes = algorithm.ComputeHash(input);
            return BitConverter.ToString(input);
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool IsValidUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                return true;
            }
            return false;
        }

        public static bool IsAlphanumeric(string str)
        {
            if (str.All(char.IsLetterOrDigit))
            {
                return true;
            }
            return false;
        }
    }
}
