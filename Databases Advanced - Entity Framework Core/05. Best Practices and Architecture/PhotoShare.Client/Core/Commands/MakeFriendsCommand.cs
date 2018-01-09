namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Interfaces;
    using PhotoShare.Data;
    public class MakeFriendsCommand : ICommand
    {
        private const string UserNotFoundExceptionMessage = "{0} not found!";
        private const string RequestNotFoundExceptionMessage = "{0} has not added {1} as a friend";
        private const string AlreadyFriendsExceptionMessage = "{0} is already a friend to {1}";

        private const string SuccessfulAcceptedFriend = "{0} accepted {1} as a friend";

        // AcceptFriend <username1> <username2>
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            var username1 = data[0];
            var username2 = data[1];
            using (var db = new PhotoShareContext())
            {
                var user1 = db.Users.FirstOrDefault(x => x.Username == username1);
                if (user1 == null)
                {
                    throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, username1));
                }
                var user2 = db.Users.FirstOrDefault(x => x.Username == username2);
                if (user2 == null)
                {
                    throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, username2));
                }
                var friendship = db.Friendships
                    .SingleOrDefault(f => f.User.Id == user1.Id && f.Friend.Id == user2.Id);
                if (friendship == null)
                {
                    throw new InvalidCastException(string.Format(RequestNotFoundExceptionMessage, user1.Username, user2.Username));
                }

                if (friendship.IsAccepted)
                {
                    throw new InvalidCastException(string.Format(AlreadyFriendsExceptionMessage, user1.Username, user2.Username));
                }
                user1.FriendsAdded.Add(friendship);

                friendship.IsAccepted = true;
                db.SaveChanges();
            }
            return string.Format(SuccessfulAcceptedFriend, username1, username2);
        }
    }
}
