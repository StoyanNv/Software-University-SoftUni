namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;

    class CreateTeam : ICommand
    {
        public string Execute(string[] data)
        {
            var name = data[0];
            var acronym = data[1];
            string description = null;
            if (data.Length == 3)
            {
                description = data[2];
            }

            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LogoutFirst);
            }
            if (acronym.Length != 3)
            {
                throw new ArgumentException($"Acronym {acronym} not valid!");
            }
            using (var db = new TeamBuilderContext())
            {
                if (db.Teams.Any(e => e.Name == name))
                {
                    throw new ArgumentException($"Team {name} exists!");
                }
                var team = new Team()
                {
                    CreatorId = AuthenticationManager.GetCurrentUser().Id,
                    Name = name,
                    Acronym = acronym,
                    Description = description
                };
                db.Teams.Add(team);
                db.SaveChanges();
            }
            return $"Team {name} successfully created!";
        }
    }
}
