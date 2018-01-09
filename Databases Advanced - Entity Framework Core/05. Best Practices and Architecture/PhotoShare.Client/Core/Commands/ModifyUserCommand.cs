namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Data;
    using ICommand = PhotoShare.Client.Interfaces.ICommand;
    public class ModifyUserCommand : ICommand
    {
        private const string UserNotFoundExceptionMessage = "User {0} not found!";
        private const string ForbiddenOrNotSupportedModificationExceptionMessage = "Property {0} not supported!";
        private readonly string exceptionMessage = "Value {0} not valid" + Environment.NewLine;
        private const string townError = "Town {0} not found!";
        private const string SuccessfulModiffication = "User {0} {1} is {2}.";

        // ModifyUser <username> <property> <new value>
        // For example:
        // ModifyUser <username> Password <NewPassword>
        // ModifyUser <username> BornTown <newBornTownName>
        // ModifyUser <username> CurrentTown <newCurrentTownName>
        // !!! Cannot change username
        public string Execute(string[] data)
        {
            string username = data[0];
            string property = data[1];
            string newValue = data[2];

            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            using (var db = new PhotoShareContext())
            {
                var user = db.Users.Where(u => u.Username == username).FirstOrDefault();

                if (user == null)
                {
                    throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, username));
                }

                switch (property)
                {
                    case "password":
                        if (!newValue.Any(c => Char.IsLower(c)) || !newValue.Any(c => Char.IsDigit(c)))
                        {
                            throw new ArgumentException(exceptionMessage + "Invalid Password");
                        }
                        user.Password = newValue;
                        break;
                    case "borntown":
                        var bornTown = db.Towns.Where(x => x.Name == newValue).FirstOrDefault();

                        if (bornTown == null)
                        {
                            throw new ArgumentException(string.Format(exceptionMessage, newValue) + townError);
                        }
                        user.BornTown = bornTown;
                        break;
                    case "currenttown":
                        var currenttown = db.Towns.Where(x => x.Name == newValue).FirstOrDefault();

                        if (currenttown == null)
                        {
                            throw new ArgumentException(string.Format(exceptionMessage, newValue) + townError);
                        }
                        user.CurrentTown = currenttown;
                        break;

                    default:
                        throw new ArgumentException(string.Format(ForbiddenOrNotSupportedModificationExceptionMessage, property));
                }
                db.SaveChanges();
            }
            return string.Format(SuccessfulModiffication, username, property, newValue);
        }
    }
}