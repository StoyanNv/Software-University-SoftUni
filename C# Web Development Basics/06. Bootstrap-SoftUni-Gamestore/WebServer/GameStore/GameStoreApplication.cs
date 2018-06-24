namespace HTTPServer.GameStore
{
    using Controllers;
    using Data;
    using Server.Contracts;
    using Server.Routing.Contracts;
    using Microsoft.EntityFrameworkCore;
    public class GameStoreApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            ConfigureDatabase();
            ConfigureRoutes(appRouteConfig);
        }

        private void ConfigureRoutes(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig.Get("/", req => new HomeController().Index(req));

            appRouteConfig.Get("/register", req => new AccountController().Register());

            appRouteConfig.Post("/register", req => new AccountController().Register(req));

            appRouteConfig.Get("/login", req => new AccountController().Login());

            appRouteConfig.Post("/login", req => new AccountController().Login(req));

            appRouteConfig.Get("/logout", req => new AccountController().Logout(req));

            appRouteConfig.Get("/games", req => new AdminController().ShowGames(req));

            appRouteConfig.Get("/add", req => new AdminController().AddGame(req));

            appRouteConfig.Post("/add", req => new AdminController().AddGamePost(req));

            appRouteConfig.Get("/delete/{(?<id>[0-9]+)}", req => new AdminController()
                    .Delete(req, int.Parse(req.UrlParameters["id"])));

            appRouteConfig.Post("/delete/{(?<id>[0-9]+)}", req => new AdminController()
                .DeletePost(int.Parse(req.UrlParameters["id"])));

            appRouteConfig.Get("/edit/{(?<id>[0-9]+)}", req => new AdminController()
                .Edit(req, int.Parse(req.UrlParameters["id"])));

            appRouteConfig.Post("/edit/{(?<id>[0-9]+)}", req => new AdminController()
                .EditPost(req, int.Parse(req.UrlParameters["id"])));

            appRouteConfig.Get("/info/{(?<id>[0-9]+)}", req => new GameController()
                .Information(req, int.Parse(req.UrlParameters["id"])));

            appRouteConfig.Get("/owned", req => new HomeController()
                .GetOwnedGames(req));


            appRouteConfig.Get("/cart", req => new GameController()
                .Cart(req));

            appRouteConfig.Get("/buy/{(?<id>[0-9]+)}", req => new GameController()
                .AddToCart(req, int.Parse(req.UrlParameters["id"])));

            appRouteConfig.Get("/clear/{(?<id>[0-9]+)}", req => new GameController()
                .Clear(req, int.Parse(req.UrlParameters["id"])));

            appRouteConfig.Post("/cart", req => new GameController()
                .FinishOrder(req));
        }
        private void ConfigureDatabase()
        {
            GameStoreContext context = new GameStoreContext();
            context.Database.Migrate();
        }
    }
}
