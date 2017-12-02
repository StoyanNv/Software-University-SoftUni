namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using PhotoShare.Client.Interfaces;
    using PhotoShare.Data;
    using PhotoShare.Models;

    public class UploadPictureCommand : ICommand
    {
        private const string AlbumNotFoundExceptionMessage = "Album {0} not found!";

        private const string SuccessfulAddedPicture = "Picture {0} added to {1}!";

        // UploadPicture <albumName> <pictureTitle> <pictureFilePath>
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            var albumName = data[0];
            var title = data[1];
            var path = data[2];
            using (var db = new PhotoShareContext())
            {
                var album = db.Albums.FirstOrDefault(x => x.Name == albumName);
                if (album == null)
                {
                    throw new ArgumentException(string.Format(AlbumNotFoundExceptionMessage, albumName));
                }
                db.Pictures.Add(new Picture()
                {
                    Album = album,
                    Path = path,
                    Title = title
                });
                db.SaveChanges();

                return string.Format(SuccessfulAddedPicture, title, album.Name);
            }
        }
    }
}
