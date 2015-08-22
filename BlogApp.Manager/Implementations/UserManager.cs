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
        private IRoleDataAccess roleDataAccess;

       public UserManager(IUserDataAccess userDataAccess, IRoleDataAccess roleDataAccess)
        {
            this.userDataAccess = userDataAccess;
            this.roleDataAccess = roleDataAccess;
        }

        public void AddUser(User user)
        {
            var existsUser = GetUserByUsername(user.Username);
            if (existsUser == null)
            {
                user.IsActive = true;
                var role = roleDataAccess.GetRoleByDescription("Blogger");
                user.Roles = new List<Role>(){ role };
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
                if(existingUser != null)
                {
                    string hashedPassword = GetHash(user);
                    if (existingUser.Password.Equals(hashedPassword))
                    {
                        user = existingUser;
                    }
                    return true;
                }
            }
            return false;
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

        public List<Tuple<string, string>> ValidateUser(User user)
        {
            List<Tuple<string, string>> errors = new List<Tuple<string, string>>();

            if (user.Name == null || user.Name.Equals(String.Empty))
            {
                errors.Add(new Tuple<string, string>("Name", "The Name field is required"));
            }

            if (user.Surname == null || user.Surname.Equals(String.Empty))
            {
                errors.Add(new Tuple<string, string>("Surname", "The Surname field is required"));
            }

            if (user.Username == null || user.Username.Equals(String.Empty))
            {
                errors.Add(new Tuple<string, string>("Username", "The Username field is required"));
            }

            if (user.Password == null || user.Password.Equals(String.Empty))
            {
                errors.Add(new Tuple<string, string>("Password", "The Password field is required"));
            }

            try
            {
                var addr = new System.Net.Mail.MailAddress(user.Email);
            }
            catch
            {
                errors.Add(new Tuple<string, string>("Email", "The Email field is required, Ej: jack@sparrow.com"));
            }

            return errors;
        }

        public string GetHash(User user)
        {
            byte[] input = Encoding.UTF8.GetBytes(user.Password);
            HashAlgorithm algorithm = new MD5CryptoServiceProvider();
            byte[] hashedBytes = algorithm.ComputeHash(input);
            return BitConverter.ToString(input);
            //return Util.GetHash(user.Password);
        }

        public List<User> GetUsers()
        {
            return userDataAccess.GetUsers();
        }
    }
}
