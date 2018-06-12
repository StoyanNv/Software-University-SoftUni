using HTTPServer.ByTheCake;
using HTTPServer.Server;
using HTTPServer.Server.Routing;

namespace HTTPServer
{
    internal class Launcher
    {
        private static void Main(string[] args)
        {
            Run(args);
        }

        private static void Run(string[] args)
        {
            var mainApplication = new ByTheCakeApplication();
            var appRouteConfig = new AppRouteConfig();
            //TODO: Configure App Route Configuration
            mainApplication.Configure(appRouteConfig);
            var server = new WebServer(8000, appRouteConfig);

            server.Run();
        }
    }
}