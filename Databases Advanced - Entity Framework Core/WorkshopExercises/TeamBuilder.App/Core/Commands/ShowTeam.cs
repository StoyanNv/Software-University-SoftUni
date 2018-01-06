namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;
    class ShowTeam : ICommand
    {
        public string Execute(string[] data)
        {
            var teamName = data[0];
            var sb = new StringBuilder();
            using (var db = new TeamBuilderContext())
            {
                var team = db.Teams.FirstOrDefault(e => e.Name == teamName);
                if (team == null)
                {
                    throw new ArgumentException($"Event {teamName} not found!");
                }
                sb.AppendLine($"{teamName} {team.Acronym}");

                sb.AppendLine("Members:");
                
                foreach (var member in db.UsersTeams.Where(x => x.TeamId == team.Id).Select(x => x.TeamId).ToArray())
                {
                    sb.AppendLine($"-{db.Teams.FirstOrDefault(t => t.Id == member).Name}");
                }
            }
            return sb.ToString().Trim();
        }
    }
}
