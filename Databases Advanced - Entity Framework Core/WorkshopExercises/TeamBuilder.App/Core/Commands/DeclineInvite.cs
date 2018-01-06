namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    class DeclineInvite : ICommand
    {
        public string Execute(string[] data)
        {

            var teamName = data[0];
            if (!AuthenticationManager.IsAuthenticated())
            {
                throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
            }
            using (var db = new TeamBuilderContext())
            {
                var team = db.Teams.FirstOrDefault(t => t.Name == teamName);
                if (team == null)
                {
                    throw new ArgumentException($"Team {teamName} not found!");
                }
                var invitaition = db.Invitations
                    .SingleOrDefault(i => i.TeamId == team.Id && i.InvitedUserId == AuthenticationManager.GetCurrentUser().Id);

                if (invitaition == null || invitaition.IsActive == false)
                {
                    throw new ArgumentException($"Invite from {teamName} is not found!");
                }
                invitaition.IsActive = false;

                db.SaveChanges();
                return $"Invite from {teamName} declined.";
            }
        }
    }
}
