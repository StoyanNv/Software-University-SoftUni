namespace PhotoShare.Client.Core.Commands
{
    using System;
    using System.Linq;
    using Models;
    using Data;
    using Utilities;
    using PhotoShare.Client.Interfaces;

    public class AddTagCommand : ICommand
    {
        private const string TagExistsExceptionMessage = "Tag {0} exists!";
        private const string SuccessfulAddedTag = "{0} was added successfully to database!";
        public string Execute(string[] data)
        {
            string tagName = data[0].ValidateOrTransform();

            using (PhotoShareContext context = new PhotoShareContext())
            {
                if (Session.User == null)
                {
                    throw new ArgumentException("Invalid Credentials!");
                }
                var tag = context.Tags
                    .SingleOrDefault(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase));

                if (tag != null)
                {
                    throw new ArgumentException(string.Format(TagExistsExceptionMessage, tagName));
                }
                context.Tags.Add(new Tag
                {
                    Name = tagName
                });

                context.SaveChanges();
            }
            return string.Format(SuccessfulAddedTag, tagName);
        }
    }
}
