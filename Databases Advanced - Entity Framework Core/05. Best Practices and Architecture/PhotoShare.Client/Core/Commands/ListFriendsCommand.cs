namespace PhotoShare.Client.Core.Commands
{
    using System;
    using PhotoShare.Client.Interfaces;
    using PhotoShare.Data;
    using System.Linq;
    using System.Text;
    public class ListFriendsCommand : ICommand
    {
        // PrintFriendsList <username>
        private const string UserNotFoundException = "User {0} not found!";
        public string Execute(string[] data)
        {
            var username = data[0];
            using (var db = new PhotoShareContext())
            {
                var user = db.Users.FirstOrDefault(x => x.Username == username);
                if (user == null)
                {
                    throw new ArgumentException(string.Format(UserNotFoundException, username));
                }
                if (!db.Friendships.Where(x => x.User.Username == username).Any())
                {
                    return "No friends for this user. :(";
                }
                var sb = new StringBuilder();
                sb.AppendLine("Friends:");
                foreach (var friend in db.Friendships.Where(u => u.User.Username == username))
                {
                    sb.AppendLine("-" + db.Users.FirstOrDefault(x => x.Id == friend.FriendId).Username);
                }
                return sb.ToString();
            }
        }
    }
}
