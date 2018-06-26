namespace SimpleMvc.App.Views.Users
{
    using Framework.Interfaces.Generics;
    using System.Text;
    using ViewModels;

    public class All : IRenderable<AllUsernamesViewModel>
    {
        public string Render()
        {
            var sb = new StringBuilder();
            sb.AppendLine("<a href =\"/home/index\">Home</a>");
            sb.AppendLine("<h3> All users </h3>");
            sb.AppendLine("<ul>");
            foreach (var username in Model.Usernames)
            {
                sb.AppendLine($"<li><a href=\"/users/profile/?id={username.Key}\">{username.Value}</a></li>");
            }
            sb.AppendLine("</ul>");

            return sb.ToString();
        }

        public AllUsernamesViewModel Model { get; set; }
    }
}