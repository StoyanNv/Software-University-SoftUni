namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    class Login : ICommand
    {
        public string Execute(string[] data)
        {
            Check.CheckLength(2, data);

            var username = data[0];
            var password = data[1];

            if (AuthenticationManager.IsAuthenticated())
            {
                throw new ArgumentException(Constants.ErrorMessages.LogoutFirst);
            }
            User user = this.GetUserByCredentials(username, password);
            if (user == null||user.IsDeleted == true)
            {
                throw new ArgumentException(Constants.ErrorMessages.UserOrPasswordIsInvalid);
            }
            AuthenticationManager.Login(user);
            return $"User {user.Username} successfully logged in!";

        }

        private User GetUserByCredentials(string username, string password)
        {
            using (var db = new TeamBuilderContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
                return user;
            }
        }
    }
}
