namespace TeamBuilder.App.Core.Commands
{
    using System;
    using System.Linq;
    using System.Text;
    using TeamBuilder.App.Utilities;
    using TeamBuilder.Data;

    class ShowEvent : ICommand
    {
        public string Execute(string[] data)
        {
            var eventName = data[0];
            var sb = new StringBuilder();
            using (var db = new TeamBuilderContext())
            {
                var eventt = db.Events.OrderByDescending(e=>e.StartDate).FirstOrDefault(e => e.Name == eventName);
                if (eventt == null)
                {
                    throw new ArgumentException($"Event {eventName} not found!");
                }
                sb.AppendLine($"{eventName} {eventt.StartDate} {eventt.EndDate}");
                if (eventt.Description != null)
                {
                    sb.AppendLine(eventt.Description);
                }
                sb.AppendLine("Teams:");
              
                foreach (var team in db.EventsTeams.Where(x => x.EventId == eventt.Id).Select(x=>x.TeamId).ToArray())
                {
                    sb.AppendLine($"-{db.Teams.FirstOrDefault(t=>t.Id==team).Name}");
                }
            }
            return sb.ToString().Trim();
        }
    }
}
