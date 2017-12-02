namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Interfaces;
    class LogOutCommand : ICommand
    {
        private const string ThereIsNoUserLoggedIn = "You should log in first in order to logout.";
        private const string Success = "User {0} successfully logged out!";
        public string Execute(string[] data)
        {
            var username = string.Empty;
            if (Session.User != null)
            {
                username = Session.User.Username;
                Session.User = null;
            }
            else
            {
                throw new ArgumentException(ThereIsNoUserLoggedIn);
            }
            return string.Format(Success, username);
        }
    }
}
