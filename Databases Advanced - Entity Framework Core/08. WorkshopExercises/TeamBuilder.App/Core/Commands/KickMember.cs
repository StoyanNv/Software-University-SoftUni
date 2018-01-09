namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    class KickMember : ICommand
    {
        public string Execute(string[] data)
        {
            var teamName = data[0];
            var username = data[1];
            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            using (var db = new TeamBuilderContext())
            {
                var user = db.Users.FirstOrDefault(u => u.Username == username);
                var team = db.Teams.FirstOrDefault(t => t.Name == teamName);
                if (user == null)
                {
                    throw new ArgumentException($"User {username} not found!");
                }
                if (team == null)
                {
                    throw new ArgumentException($"Team {teamName} not found!");
                }
                if (!team.TeamMembers.Any(x => x.UserId == user.Id))
                {
                    throw new ArgumentException($"User {username} is not a member in {teamName}!");
                }
                if (team.CreatorId != AuthenticationManager.GetCurrentUser().Id)
                {
                    throw new InvalidOperationException("Not allowed!");
                }
                if (user.Id == team.CreatorId)
                {
                    throw new InvalidOperationException("Command not allowed. Use DisbandTeam instead.");
                }
                var mapping = team.TeamMembers.FirstOrDefault(tm => tm.TeamId == team.Id && tm.UserId == user.Id);
                db.Remove(mapping);
                db.SaveChanges();
            }
            return $"User {username} was kicked from {teamName}!";
        }
    }
}