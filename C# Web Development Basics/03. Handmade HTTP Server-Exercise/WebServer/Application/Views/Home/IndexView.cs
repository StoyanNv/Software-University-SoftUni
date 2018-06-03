using WebServer.Server.Contracts;

namespace WebServer.Application.Views
{
    public class IndexView :IView
    {
        public string View() => "<h1>Welcome!</h1>";
    }
}