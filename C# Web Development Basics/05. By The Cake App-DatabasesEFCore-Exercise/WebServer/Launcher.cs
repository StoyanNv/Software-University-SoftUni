namespace HTTPServer
{
    using ByTheCake;
    using Server;
    using Server.Routing;
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
            mainApplication.Configure(appRouteConfig);
            var server = new WebServer(8000, appRouteConfig);

            server.Run();
        }
    }
}