namespace TeamBuilder.App.Utilities
{
    using System.Linq;
    using TeamBuilder.Data;
    using TeamBuilder.Models;
    class CommandHelper
    {
        public static bool IsTeamExisting(string teamName)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Teams.Any(t => t.Name == teamName);
            }
        }

        public static bool IsUserExisting(string username)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Users.Any(t => t.Username == username && t.IsDeleted == false);
            }
        }

        public static bool IsInviteExisting(string teamName, User user)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Invitations
                    .Any(i => i.Team.Name == teamName && i.InvitedUserId == user.Id && i.IsActive);
            }
        }

        public static bool IsUserCreatorOfTeam(string teamName, User user)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Teams
                    .FirstOrDefault(t => t.Name == teamName)
                    .CreatorId == user.Id;
            }
        }

        public static bool IsUserCreatorOfEvent(string eventName, User user)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Events
                           .FirstOrDefault(t => t.Name == eventName)
                           .CreatorId == user.Id;
            }
        }

        public static bool IsMemberOfTeam(string teamName, string username)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Teams
                    .Single(t => t.Name == teamName)
                    .TeamMembers.Any(ut => ut.User.Username == username);
            }
        }

        public static bool IsEventExisting(string eventName)
        {
            using (var db = new TeamBuilderContext())
            {
                return db.Events.Any(e => e.Name == eventName);
            }
        }
    }
}
