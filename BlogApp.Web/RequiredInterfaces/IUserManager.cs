using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BlogApp.Models;
namespace BlogApp.Web.RequiredInterfaces
{
    public interface IUserManager
    {
        void AddUser(User user, List<RoleType> roles, Byte[] ImageBytes);
        void ModifyUser(User user, Byte[] ImageByte);
        void RemoveUser(User user);
        bool ValidateLogin(ref User user);
        bool ValidateRegistration(User user);
        List<Tuple<string, string>> ValidateUser(User user);
        string GetHash(User user);
        User GetUserByUsername(string username);
        User GetUserById(int userId);
        List<User> GetUsers();
        List<Tuple<User, int>> GetMostActiveUsers(DateTime fromDate, DateTime toDate);
    }
}
