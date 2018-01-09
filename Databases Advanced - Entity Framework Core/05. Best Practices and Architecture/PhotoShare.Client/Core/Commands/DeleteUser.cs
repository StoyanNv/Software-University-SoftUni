namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Data;
    using PhotoShare.Client.Interfaces;

    public class DeleteUser : ICommand
    {
        private const string UserAlreadyDeletedExceptionMessage = "User {0} is already deleted!";
        private const string UserNotFoundExceptionMessage = "User {0} not found!";
        private const string SuccessfulDeletion = "User {0} was deleted successfully!";
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            string username = data[0];
            using (PhotoShareContext context = new PhotoShareContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Username == username);
                if (user == null)
                {
                    throw new InvalidOperationException(string.Format(UserNotFoundExceptionMessage, username));
                }

                if (user.IsDeleted.Value)
                {
                    throw new InvalidOperationException(string.Format(UserAlreadyDeletedExceptionMessage, username));
                }

                user.IsDeleted = true;
                context.SaveChanges();

                return string.Format(SuccessfulDeletion, username);
            }
        }
    }
}