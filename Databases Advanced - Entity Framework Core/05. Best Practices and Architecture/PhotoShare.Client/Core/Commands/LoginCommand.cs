using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PhotoShare.Data;
using PhotoShare.Models;

namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Interfaces;

    class LoginCommand : ICommand
    {
        private const string userDoesNotExistOrPasswordDoesNotMatch = "Invalid username or password!";
        private const string UserHasLoggedInAlready = "You should logout first!";
        private const string Success = "User {0} successfully logged in!";
        public string Execute(string[] data)
        {
            var username = data[0];
            var password = data[1];

            var user = VerifyUser(username, password);

            if (Session.User == null)
            {
                Session.User = user;
            }
            else
            {
                throw new ArgumentException(UserHasLoggedInAlready);
            }
            return string.Format(Success, username);

        }

        private User VerifyUser(string username, string password)
        {
            using (var db = new PhotoShareContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                if (user == null)
                {
                    throw new ArgumentException(userDoesNotExistOrPasswordDoesNotMatch);
                }
                return user;
            }
        }
    }
}
