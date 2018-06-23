namespace HTTPServer.ByTheCake
{
    using Data;
    using Controllers;
    using Microsoft.EntityFrameworkCore;
    using Server.Contracts;
    using Server.Routing.Contracts;

    public class ByTheCakeApplication : IApplication
    {
        public void Configure(IAppRouteConfig appRouteConfig)
        {
            ConfigureDatabase();
            ConfigureRoutes(appRouteConfig);
        }

        public static void ConfigureRoutes(IAppRouteConfig appRouteConfig)
        {
            appRouteConfig
                .Get("/", req => new HomeController().Index());

            appRouteConfig
                .Get("/about", req => new HomeController().About(req));

            appRouteConfig
               .Get("/profile", req => new AccountController().Profile(req));

            appRouteConfig
                .Get("/add", req => new CakesController().Add());

            appRouteConfig
                .Post("/add", req => new CakesController()
                .Add(req.FormData["name"], req.FormData["price"], req.FormData["imageUrl"]));

            appRouteConfig
                .Get("/search", req => new CakesController().Search(req));

            appRouteConfig
                .Get("/cakeDetails/{(?<id>[0-9]+)}", req => new CakesController()
                .Details(int.Parse(req.UrlParameters["id"])));

            appRouteConfig
                .Get("/register", req => new AccountController().Register());

            appRouteConfig
               .Post("/register", req => new AccountController().Register(req));

            appRouteConfig
                .Get("/login", req => new AccountController().Login());

            appRouteConfig
                .Post("/login", req => new AccountController().Login(req));

            appRouteConfig
                .Post("/logout", req => new AccountController().Logout(req));

            appRouteConfig
                .Get("/shopping/add/{(?<id>[0-9]+)}", req => new ShoppingController()
                .AddToCart(req));

            appRouteConfig
                .Get("/cart", req => new ShoppingController().ShowCart(req));

            appRouteConfig
                .Get("/orders", req => new ShoppingController().ShowOrders(req));
            appRouteConfig
                .Get("/orderDetails/{(?<id>[0-9]+)}", req => new ShoppingController()
                    .OrderDetails(int.Parse(req.UrlParameters["id"])));
            appRouteConfig
                .Post("/shopping/finish-order", req => new ShoppingController()
                .FinishOrder(req));
        }

        public static void ConfigureDatabase()
        {
            ByTheCakeContext context = new ByTheCakeContext();
            context.Database.Migrate();
        }
    }
}