using BlogApp.Manager.RequiredInterfaces;
using BlogApp.Models;
using BlogApp.Web.RequiredInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
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

        public void AddUser(User user, List<RoleType> roles, Byte[] ImageBytes)
        {
            if (ImageBytes != null)
            {
                user.PicturePath = BytesImage(ImageBytes);
            }
            else
            {
                user.PicturePath = String.Empty;
            }
            user.IsActive = true;
            user.Roles = roles.Select(r => roleDataAccess.GetRoleByType(r)).ToList();
            userDataAccess.AddUser(user);         
        }

        public void ModifyUser(User user,List<RoleType> roles, Byte[] ImageBytes)
        {
            if (ImageBytes != null)
            {
                user.PicturePath = BytesImage(ImageBytes);
            }
            else
            {
                User usAux = userDataAccess.GetUserById(user.Id);
                if (usAux.PicturePath.Equals(String.Empty))
                {
                    user.PicturePath = String.Empty;
                }
                else
                {
                    user.PicturePath = usAux.PicturePath;
                }
            }
            user.Roles = roles.Select(r => roleDataAccess.GetRoleByType(r)).ToList();
            userDataAccess.ModifyUser(user);
        }

        private string BytesImage(Byte[] ImgBytes)
        {
            string name = Guid.NewGuid().ToString() + ".jpg";
            string path = System.AppDomain.CurrentDomain.BaseDirectory + "ProfilePictures/" + name;
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            File.WriteAllBytes(path, ImgBytes);
            path = "/ProfilePictures/" + name;
            return path;
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

        public User GetUserById(int userId)
        {
            return userDataAccess.GetUserById(userId);
        }

        public List<Tuple<User, int>> GetMostActiveUsers(DateTime fromDate, DateTime toDate)
        {
            return userDataAccess.GetMostActiveUsers(fromDate, toDate);
        }
        public void UpdateUserComments(int userId, Comment comment)
        {
            userDataAccess.UpdateUserComments(userId, comment);
        }
        public int CountAdmin()
        {
            return userDataAccess.CountAdmin();
        }
    }
}
