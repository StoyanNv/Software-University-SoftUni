namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using System.Threading;
    using PhotoShare.Client.Interfaces;
    using PhotoShare.Data;
    using PhotoShare.Models;
    public class AddTagToCommand : ICommand
    {
        private const string AlbumOrTagDoesNotExist = "Either tag or album do not exist!";
        private const string Success = "Tag {0} added to {1}!";
        // AddTagTo <albumName> <tag>
        public string Execute(string[] data)
        {
            if (Session.User == null)
            {
                throw new ArgumentException("Invalid Credentials!");
            }
            var albumName = data[0];
            var tagName = "#" + data[1];
            using (var db = new PhotoShareContext())
            {
                var album = db.Albums.FirstOrDefault(x => x.Name == albumName);
                if (album == null)
                {
                    throw new ArgumentException(AlbumOrTagDoesNotExist);
                }
                var tag = db.Tags.FirstOrDefault(x => x.Name == tagName);
                if (tag == null)
                {
                    throw new ArgumentException(AlbumOrTagDoesNotExist);
                }
                db.Add(new AlbumTag(album, tag));
                db.SaveChanges();
            }
            return string.Format(Success, tagName, albumName);
        }
    }
}