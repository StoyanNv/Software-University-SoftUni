namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PhotoShare.Client.Interfaces;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class CreateAlbumCommand : ICommand
    {
        private const string invalidTagsException = "Invalid tags!";
        private const string ColorNotExistsExceptionMessage = "Color {0} not found!";
        private const string AlbumExistsException = "Album {0} exists!";
        private const string UserDoesNotExistException = "User {0} not found!";
        private const string SuccessAddition = "Album {0} successfully created!";
        private const Role UserRole = Role.Owner;

        // CreateAlbum <username> <albumTitle> <BgColor> <tag1> <tag2>...<tagN>
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            var username = data[0];
            var albumTitle = data[1];
            var BgColor = data[2];
            var tags = data.Skip(3).ToArray();
            var tagsList = new List<Tag>();
            using (var db = new PhotoShareContext())
            {

                foreach (var tag in tags)
                {
                    var temp = db.Tags.FirstOrDefault(x => x.Name.Equals("#" + tag));
                    if (temp == null)
                    {
                        throw new ArgumentException(invalidTagsException);
                    }
                    tagsList.Add(temp);
                }
                if (db.Albums.FirstOrDefault(x => x.Name == albumTitle) != null)
                {
                    throw new ArgumentException(string.Format(AlbumExistsException, albumTitle));
                }
                var album = new Album()
                {
                    Name = albumTitle,
                    BackgroundColor = ParseColor(BgColor),
                };

                var user = db.Users.FirstOrDefault(x => x.Username == username);
                if (user == null)
                {
                    throw new ArgumentException(string.Format(UserDoesNotExistException, username));
                }
                user.AlbumRoles.Add(new AlbumRole() { User = user, Album = album, Role = UserRole });
                foreach (var tag in tagsList)
                {
                    db.AlbumTags.Add(new AlbumTag(album, tag));
                }
                db.SaveChanges();
                return string.Format(SuccessAddition, albumTitle);
            }
        }
        private Color ParseColor(string BgColor)
        {
            Color color;
            var isColorAvailable = Enum.TryParse(BgColor, true, out color);
            if (!isColorAvailable)
            {
                throw new ArgumentException(string.Format(ColorNotExistsExceptionMessage, BgColor));
            }

            return color;
        }
    }
}