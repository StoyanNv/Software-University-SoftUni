namespace TeamBuilder.App.Core.Commands
{
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    class DeleteUser : ICommand
    {
        public string Execute(string[] data)
        {
            Check.CheckLength(0, data);

            AuthenticationManager.Authorize();

            var currUser = AuthenticationManager.GetCurrentUser();

            using (var db = new TeamBuilderContext())
            {
                var user = db.Users.
                     FirstOrDefault(u => u.Id == currUser.Id);

                user.IsDeleted = true;
                currUser.IsDeleted = true;
                db.SaveChanges();
                AuthenticationManager.Logout();
            }
            return $"User {currUser.Username} was deleted successfully!";

        }
    }
}
