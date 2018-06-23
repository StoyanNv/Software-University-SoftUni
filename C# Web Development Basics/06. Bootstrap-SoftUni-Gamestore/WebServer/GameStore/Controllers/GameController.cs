using System.Collections.Generic;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.GameStore.Controllers
{
    using Infrastructure;
    using Models;
    using Server.Http;
    using Server.Http.Contracts;
    using System;
    using System.Linq;
    class GameController : Controller
    {
        public IHttpResponse Information(IHttpRequest req, int id)
        {
            User user = null;
            Game game = Context.Games.FirstOrDefault(g => g.Id == id);
            this.ViewData["title"] = game.Title;
            this.ViewData["video"] = game.Trailer;
            this.ViewData["description"] = game.Description;
            this.ViewData["price"] = $"{game.Price:f2}";
            this.ViewData["size"] = $"{game.Size:f2}";
            this.ViewData["date"] = game.ReleaseDate.ToString("yyyy-MM-dd");
            this.ViewData["showAdminBtn"] = "none";
            this.ViewData["gameId"] = game.Id.ToString();

            try
            {
                var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);

                user = Context.Users.FirstOrDefault(u => u.Id == currentUserId);
            }
            catch (Exception)
            {
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                return this.FileViewResponse(@"home\index");
            }


            if (user.Administrator)
            {
                this.ViewData["showAdminBtn"] = "inline";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
            }
            else
            {
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "block";
                this.ViewData["adminHeader"] = "none";
            }

            return this.FileViewResponse(@"games\game-details");
        }

        public IHttpResponse Cart(IHttpRequest req)
        {
            List<Game> shoppingCart;
            try
            {
                shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders;

            }
            catch (Exception e)
            {
                shoppingCart = new List<Game>();
            }

            var html = "";
            foreach (var game in shoppingCart)
            {
                html += $"<div class=\"list-group-item\">" +
                       $"<div class=\"media\">" +
                       $"<a class=\"btn btn-outline-danger btn-lg align-self-center mr-3\" href=\"\\clear\\{game.Id}\">X</a>" +
                       $"<img class=\"d-flex mr-4 align-self-center img-thumbnail\" height=\"127\" src=\"{game.ImageThumbnail}\"" +
                       $" width=\"227\" alt=\"Generic placeholder image\">" +
                       $"<div class=\"media-body align-self-center\">" +
                       $"<a href=\"\\info\\{game.Id}\">" +
                       $"<h4 class=\"mb-1 list-group-item-heading\"> {game.Title} </h4></a>" +
                       $"<p>{game.Description}</p> " +
                       $"</div> " +
                       $"<div class=\"col-md-2 text-center align-self-center mr-auto\">" +
                       $"<h2> {game.Price}€;</h2>" +
                       $"</div >" +
                       $"</div >" +
                       $"</div >";
            }

            User user = null;
            try
            {
                var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
                user = Context.Users.FirstOrDefault(u => u.Id == currentUserId);
                this.ViewData["total"] = shoppingCart.Select(g => g.Price).Sum().ToString();
                this.ViewData["shoppingCart"] = html;
            }
            catch (Exception)
            {
                this.ViewData["shoppingCart"] = html;
                this.ViewData["total"] = "0";
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                return this.FileViewResponse(@"games\cart");
            }


            if (user.Administrator)
            {
                this.ViewData["total"] = shoppingCart.Select(g => g.Price).Sum().ToString();
                this.ViewData["shoppingCart"] = html;
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
            }
            else
            {

                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "block";
                this.ViewData["adminHeader"] = "none";
            }

            return this.FileViewResponse(@"games\cart");
        }

        public IHttpResponse AddToCart(IHttpRequest req, int id)
        {
            Game game = null;

            game = Context.Games.Find(id);

            if (game == null)
            {
                return new NotFoundResponse();
            }
            var shoppingCart = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey);
            var gameIds = shoppingCart.Orders.Where(g => g.Id == id).Count();
            if (gameIds == 0)
            {
                shoppingCart.Orders.Add(game);
            }


            return new RedirectResponse("/cart");
        }

        public IHttpResponse Clear(IHttpRequest req, int id)
        {
            req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.RemoveAll(x => x.Id == id);

            return new RedirectResponse("/cart");
        }
        public IHttpResponse FinishOrder(IHttpRequest req)
        {
            var currentUserId = 0;
            try
            {
                currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);

            }
            catch (Exception e)
            {
                return new RedirectResponse("/login");
            }
            IEnumerable<int> productIds = req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders
                .Select(o => o.Id);
            var userGames = this.Context.Orders.Where(o => o.UserId == currentUserId).Select(g => g.Games);
            foreach (var product in productIds)
            {
                foreach (var game in userGames)
                {
                    if (game.Select(x => x.GameId).Contains(product))
                    {
                        return new RedirectResponse("/");

                    }
                }
            }

            var order = new Order()
            {
                UserId = currentUserId,
                Games = productIds
                    .Select(id => new GameOrder()
                    {
                        GameId = id,
                    }).ToList()
            };
            using (Context)
            {
                Context.Orders.Add(order);
                Context.SaveChanges();
            }

            req.Session.Get<ShoppingCart>(ShoppingCart.SessionKey).Orders.Clear();
            return this.Cart(req);
        }
    }
}
