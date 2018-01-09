namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    class InviteToTeam : ICommand
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
                if (user == null || team == null)
                {
                    throw new ArgumentException("Team or user does not exist!");
                }
                if (team.CreatorId != AuthenticationManager.GetCurrentUser().Id)
                {
                    throw new InvalidOperationException("Not allowed!");
                }
                if (db.Invitations.Any(i => i.InvitedUserId == user.Id && i.TeamId == team.Id))
                {
                    throw new InvalidOperationException("Invite is already sent!");
                }
                var invitation = new Invitation()
                {
                    InvitedUserId = user.Id,
                    IsActive = true,
                    TeamId = team.Id,
                };
                db.Invitations.Add(invitation);
                db.SaveChanges();
            }

            return $"Team {teamName} invited {username}!";
        }
    }
}
