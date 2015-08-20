using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BlogApp.Manager.Implementations
{
    public class UserManager : IUserManager
    {
        private IUserDataAccess userDataAccess;

       public UserManager(IUserDataAccess userDataAccess)
        {
            this.userDataAccess = userDataAccess;
        }

        public void AddUser(User user)
        {
            var existsUser = GetUserByUsername(user.Username);
            if (existsUser == null)
            {
                user.IsActive = true;
                userDataAccess.AddUser(user);
            }
        }

        public void ModifyUser(User user)
        {
            userDataAccess.ModifyUser(user);
        }

        public void RemoveUser(User user) 
        {
            userDataAccess.RemoveUser(user);
        }

        public bool ValidateLogin(ref User user)
        {
            if (user.Username != null && user.Password != null)
            {
                var existingUser = GetUserByUsername(user.Username);
                string hashedPassword = GetHash(user);
                if (existingUser.Password.Equals(hashedPassword))
                {
                    user = existingUser;
                }
                return existingUser != null;
            }
            else
            {
                return false;
            }
        }

        public User GetUserByUsername(string username)
        {
            return userDataAccess.GetUserByUsername(username);
        }

        public bool ValidateRegistration(User user)
        {
            if (user.Name == string.Empty || user.Surname == string.Empty || user.Email == string.Empty)
                return false;
            if (user.Username == string.Empty || user.Password == string.Empty)
                return false;           

            return true;
        }

        public bool ValidateEmail(User user)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(user.Email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GetHash(User user)
        {
            byte[] input = Encoding.UTF8.GetBytes(user.Password);
            HashAlgorithm algorithm = new MD5CryptoServiceProvider();
            byte[] hashedBytes = algorithm.ComputeHash(input);
            return BitConverter.ToString(input);
            //return Util.GetHash(user.Password);
        }
    }
}
