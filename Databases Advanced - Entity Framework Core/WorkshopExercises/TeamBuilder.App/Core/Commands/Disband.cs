namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    class Disband : ICommand
    {
        public string Execute(string[] data)
        {
            var teamName = data[0];
            using (var db = new TeamBuilderContext())
            {
                var team = db.Teams.FirstOrDefault(t => t.Name == teamName);
                if (team == null)
                {
                    throw new ArgumentException($"Team {teamName} not found!");
                }
                if (team.CreatorId != AuthenticationManager.GetCurrentUser().Id)
                {
                    throw new InvalidOperationException("Not allowed!");
                }
                if (!AuthenticationManager.IsAuthenticated())
                {
                    throw new InvalidOperationException(Constants.ErrorMessages.LoginFirst);
                }
                var toDel = db.Invitations.Where(x => x.TeamId == team.Id);
                db.RemoveRange(toDel);
                db.Teams.Remove(team);
                db.SaveChanges();
            }
            return $"{teamName} has disbanded!";
        }
    }
}
