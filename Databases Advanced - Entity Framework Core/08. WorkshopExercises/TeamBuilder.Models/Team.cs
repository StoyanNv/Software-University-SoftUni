using System.Collections;
using System.Collections.Generic;

namespace TeamBuilder.Models
{
    public class Team
    {
        public Team()
        {
            Incitations = new List<Invitation>();
            TeamMembers = new List<UserTeam>();
            Events = new List<EventTeam>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Acronym { get; set; }
        public int CreatorId { get; set; }
        public User Creator { get; set; }
        public ICollection<Invitation> Incitations { get; set; }
        public ICollection<UserTeam> TeamMembers { get; set; }
        public ICollection<EventTeam> Events { get; set; }
    }
}
