namespace PhotoShare.Client.Core.Commands
{
    using Models;
    using Data;
    using System;
    using System.Linq;
    using PhotoShare.Client.Interfaces;
    public class AddTownCommand : ICommand
    {
        private const string ExistingTownExceptionMesage = "Town {0} was already added!";
        private const string SuccessfulAddedTown = "Town {0} was added successfully!";
        public string Execute(string[] data)
        {
            string townName = data[0];
            string country = data[1];
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (context.Towns.Any(x => x.Name == townName))
                {
                    throw new InvalidOperationException(string.Format(ExistingTownExceptionMesage, townName));
                }
                Town town = new Town
                {
                    Name = townName,
                    Country = country
                };

                context.Towns.Add(town);
                context.SaveChanges();

                return string.Format(SuccessfulAddedTown, townName);
            }
        }
    }
}
