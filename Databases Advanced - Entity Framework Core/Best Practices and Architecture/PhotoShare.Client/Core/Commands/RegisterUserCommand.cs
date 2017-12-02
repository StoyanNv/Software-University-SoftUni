namespace PhotoShare.Client.Core.Commands
{
    using System;
    using Models;
    using Data;
    using System.Linq;
    using PhotoShare.Client.Interfaces;

    public class RegisterUserCommand : ICommand
    {
        private const string PasswordNotMatchedExceptionMessage = "Passwords do not match!";
        private const string UsernameTakenExceptionMessage = "Username {0} is already taken!";
        private const string SuccessfulRegistrationMessage = "User {0} was registered successfully!";
        public string Execute(string[] data)
        {
            if (Session.User != null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            string username = data[0];
            string password = data[1];
            string repeatPassword = data[2];
            string email = data[3];

           
            using (var db = new PhotoShareContext())
            {
                if (db.Users.Any(x => x.Username == username))
                {
                    throw new InvalidOperationException(string.Format(UsernameTakenExceptionMessage, username));
                }
                if (password != repeatPassword)
                {
                    throw new ArgumentException(PasswordNotMatchedExceptionMessage);
                }
            }

            User user = new User
            {
                Username = username,
                Password = password,
                Email = email,
                IsDeleted = false,
                RegisteredOn = DateTime.Now,
                LastTimeLoggedIn = DateTime.Now
            };

            using (PhotoShareContext context = new PhotoShareContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            return string.Format(SuccessfulRegistrationMessage, user.Username);
        }
    }
}
