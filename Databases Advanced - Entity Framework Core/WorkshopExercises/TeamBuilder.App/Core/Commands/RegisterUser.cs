namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    class RegisterUser : ICommand
    {
        public string Execute(string[] data)
        {
            Check.CheckLength(7, data);

            var username = data[0];

            if (username.Length < Constants.MinUsernameLength ||
                username.Length > Constants.MaxUsernameLength)
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.UsernameNotValid, username));
            }
            string password = data[1];

            if (!password.Any(char.IsDigit) || !password.Any(char.IsUpper))
            {
                throw new ArgumentException(string.Format(Constants.ErrorMessages.PasswordNotValid, password));
            }
            string repeatedPassword = data[2];

            if (password != repeatedPassword)
            {
                throw new InvalidOperationException(Constants.ErrorMessages.PasswordDoesNotMatch);
            }
            var firstName = data[3];
            var lastName = data[4];

            int age;
            bool isNumber = int.TryParse(data[5], out age);

            if (!isNumber || age <= 0)
            {
                throw new ArgumentException(Constants.ErrorMessages.AgeNotValid);
            }

            Gender gender;

            bool isGenderValid = Enum.TryParse(data[6], out gender);

            if (!isGenderValid)
            {
                throw new ArgumentException(Constants.ErrorMessages.GenderNotValid);
            }
            if (CommandHelper.IsUserExisting(username))
            {
                throw new InvalidOperationException(string.Format(Constants.ErrorMessages.UsernameIsTaken, username));
            }
            this.Register(username, password, firstName, lastName, age, gender);

            return $"User {username} was registered successfully";
        }
        private void Register(string username,
            string password,
            string firstName,
            string lastName,
            int age,
            Gender gender)
        {
            using (var db = new TeamBuilderContext())
            {
                var user = new User()
                {
                    Username = username,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age,
                    Gender = gender
                };
                db.Users.Add(user);
                db.SaveChanges();
            }
        }
    }
}