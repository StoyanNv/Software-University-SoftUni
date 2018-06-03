namespace WebServer.Server.Contracts
{
    using Routing.Contracts;

    interface IApplication
    {
        void Configure(IAppRouteConfig appRouteConfig);
    }
}
