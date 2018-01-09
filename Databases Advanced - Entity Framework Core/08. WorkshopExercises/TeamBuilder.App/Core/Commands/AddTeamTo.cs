namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    class AddTeamTo: ICommand
    {
        public string Execute(string[] data)
        {
            var eventName = data[0];
            var teamName = data[1];
            using (var db = new TeamBuilderContext())
            {
                var eventt = db.Events.OrderByDescending(e => e.StartDate).FirstOrDefault(u => u.Name == eventName);
                var team = db.Teams.FirstOrDefault(t => t.Name == teamName);
                if (team == null)
                {
                    throw new ArgumentException($"Team {teamName} not found!");
                }
                if (eventt == null)
                {
                    throw new ArgumentException($"Event {eventName} not found!");
                }
                if (eventt.CreatorId != AuthenticationManager.GetCurrentUser().Id)
                {
                    throw new InvalidOperationException("Not allowed!");
                }
                if (db.EventsTeams.Any(i => i.Team.Acronym == team.Acronym && i.Event.Name == eventt.Name))
                {
                    throw new InvalidOperationException("Cannot add same team twice!");
                }
                if (!AuthenticationManager.IsAuthenticated())
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
                }
                var mapping = new EventTeam()
                {
                    Event = eventt,
                    Team = team,
                };
                db.EventsTeams.Add(mapping);
                db.SaveChanges();
            }

            return $"Team {teamName} added for {eventName}!";
        }
    }
}
