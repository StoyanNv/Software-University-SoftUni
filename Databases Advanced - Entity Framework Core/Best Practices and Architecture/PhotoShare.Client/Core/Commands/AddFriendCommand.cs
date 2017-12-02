namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Models;
    using PhotoShare.Client.Interfaces;
    using PhotoShare.Data;

    using Microsoft.EntityFrameworkCore;

    public class AddFriendCommand : ICommand
    {
        private const string UserNotFoundExceptionMessage = "User {0} not found!";
        private const string DuplicateFriendRequestExceptionMessage = "Friend request already sent!";
        private const string RequestToAFriendExceptionMessage = "{0} is already a friend to {1}";
        private const string SuccessfulRequest = "{0} sent a friend request to {1}";
        // AddFriend <username1> <username2>
        public string Execute(string[] data)
        {
            string requesterUsername = data[0];
            string addedFriendname = data[1];
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");

            }
            using (var db = new PhotoShareContext())
            {
                var requestingUser = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(x => x.Username == requesterUsername);

                if (requesterUsername == null)
                {
                    throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, requesterUsername));
                }

                var addedFriend = db.Users
                    .Include(u => u.FriendsAdded)
                    .ThenInclude(fa => fa.Friend)
                    .FirstOrDefault(u => u.Username == addedFriendname);

                if (addedFriend == null)
                {
                    throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, requesterUsername));
                }
                bool alreadyAdded = requestingUser.FriendsAdded.Any(x => x.Friend == addedFriend);

                bool accepted = addedFriend.FriendsAdded.Any(x => x.Friend == requestingUser);
                if (alreadyAdded && !accepted)
                {
                    throw new InvalidOperationException(DuplicateFriendRequestExceptionMessage);
                }
                if (alreadyAdded)
                {
                    throw new InvalidOperationException(string.Format(RequestToAFriendExceptionMessage, addedFriendname, requesterUsername));
                }
                if (!alreadyAdded && accepted)
                {
                    throw new InvalidOperationException(DuplicateFriendRequestExceptionMessage);
                }

                requestingUser.FriendsAdded
                    .Add(new Friendship()
                    {
                        User = requestingUser,
                        Friend = addedFriend
                    });
                db.SaveChanges();

                return string.Format(SuccessfulRequest, requestingUser.Username, addedFriend.Username);
            }
        }
    }
}