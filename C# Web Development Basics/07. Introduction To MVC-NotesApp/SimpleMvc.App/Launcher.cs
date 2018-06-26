namespace SimpleMvc.App
{
    using Data;
    using Framework.Routers;

    internal class Launcher
    {
        private static void Main()
        {
            var server = new WebServer.WebServer(8000, new ControllerRouter());
            Framework.MvcEngine.Run(server, new NotesDbContext());
        }
    }
}