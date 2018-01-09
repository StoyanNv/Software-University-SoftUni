namespace TeamBuilder.App.Core.Commands
{
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Models;

    class Logout : ICommand
    {
        public string Execute(string[] data)
        {
            Check.CheckLength(0, data);
            AuthenticationManager.Authorize();
            User currUser = AuthenticationManager.GetCurrentUser();

            AuthenticationManager.Logout();

            return $"User {currUser.Username} successfully logged out!";
        }
    }
}
