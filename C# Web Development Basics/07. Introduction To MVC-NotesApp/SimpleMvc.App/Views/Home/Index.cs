namespace SimpleMvc.App.Views.Home
{
    using Framework.Interfaces;

    public class Index : IRenderable
    {
        public string Render()
        {
            return "<a href=\"/users/register\">Register user </a>" +
                   "<a href=\"/users/all\">All users</a>" +
                   "<h3>Hello MVC!</h3>";
        }
    }
}