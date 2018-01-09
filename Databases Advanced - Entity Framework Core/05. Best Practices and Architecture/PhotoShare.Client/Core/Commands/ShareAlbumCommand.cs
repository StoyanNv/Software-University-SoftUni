namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Interfaces;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class ShareAlbumCommand : ICommand
    {
        // ShareAlbum <albumId> <username> <permission>
        // For example:
        // ShareAlbum 4 dragon321 Owner
        // ShareAlbum 4 dragon11 Viewer
        private const string AlbumNotFoundExceptionMessage = "Album {0} not found!";
        private const string UserNotFoundExceptionMessage = "User {0} not found!";
        private const string InvalidRoleExceptionMessage = "Permission must be either “Owner” or “Viewer”!";

        private const string SuccessfulSharedAlbum = "Username {0} added to album {1} ({2})";
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            var albumId = int.Parse(data[0]);
            var username = data[1];
            var permissionStr = data[2];

            using (var db = new PhotoShareContext())
            {
                var album = db.Albums.FirstOrDefault(x => x.Id == albumId);
                if (album == null)
                {
                    throw new ArgumentException(string.Format(AlbumNotFoundExceptionMessage, albumId));
                }
                var user = db.Users.FirstOrDefault(x => x.Username == username);
                if (user == null)
                {
                    throw new ArgumentException(string.Format(UserNotFoundExceptionMessage, username));
                }

                //  this.VerifyOwnerPermissions(album);
                var permission = this.ParsePermission(data[2]);

                db.AlbumRoles.Add(new AlbumRole()
                {
                    Album = album,
                    AlbumId = albumId,
                    Role = permission,
                    User = user,
                    UserId = user.Id
                });
                db.SaveChanges();

                return string.Format(SuccessfulSharedAlbum, user.Username, album.Name, permission.ToString());
            }
        }
        private Role ParsePermission(string name)
        {
            Role role;
            var isRoleValid = Enum.TryParse(name, true, out role);
            if (!isRoleValid)
            {
                throw new ArgumentException(InvalidRoleExceptionMessage);
            }

            return role;
        }
    }
}
