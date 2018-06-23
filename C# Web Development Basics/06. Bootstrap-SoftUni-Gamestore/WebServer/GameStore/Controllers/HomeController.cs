using System;
using System.Linq;
using HTTPServer.GameStore.Models;
using HTTPServer.GameStore.Infrastructure;
using HTTPServer.Server.Http;
using HTTPServer.Server.Http.Contracts;

namespace HTTPServer.GameStore.Controllers
{
    public class HomeController : Controller
    {
        public IHttpResponse Index(IHttpRequest req)
        {
            string cards = "";
            var games = Context.Games;
            var counter = 0;
            foreach (var game in games)
            {
                var description = game.Description;
                if (description.Length > 300)
                {
                    description = description.Substring(0, 300);
                }
                if (counter % 3 == 0)
                {
                    cards += $"<div class=\"card-group\">";
                }
                cards += $"<div class=\"card col-4 thumbnail\">" +
                         $"<img " +
                         $"class=\"card-image-top img-fluid img-thumbnail\"" +
                         $"onerror=\"this.src=\'https://i.ytimg.com/vi/{game.Trailer}/maxresdefault.jpg\';\"" +
                         $"src=\"{game.ImageThumbnail}\">" +
                         $"<div class=\"card-body\">" +
                         $"<h4 class=\"card-title\">{game.Title}</h4>" +
                         $"<p class=\"card-text\"><strong>Price</strong> - {game.Price}&euro;</p>" +
                         $"<p class=\"card-text\"><strong>Size</strong> - {game.Size} GB</p>" +
                         $"<p class=\"card-text\">{description}</p>" +
                         $"</div>" +
                         $"<div class=\"card-footer\">" +
                         $"<a class=\"card-button btn btn-outline-primary\" name=\"info\" href=\"/info/{game.Id}\">Info</a>" +
                         $"<a class=\"card-button btn btn-primary\" name=\"buy\" href=\"/buy/{game.Id}\">Buy</a>" +
                         $"</div>" +
                         $"</div>";

                if (counter == counter + 3)
                {
                    cards += "</div>";
                }
                counter++;

            }
            counter = 0;
            this.ViewData["carts"] = cards;

            User user = null;
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
                string adminCards = "";
                foreach (var game in games)
                {
                    var description = game.Description;
                    if (description.Length > 300)
                    {
                        description = description.Substring(0, 300);
                    }
                    if (counter % 3 == 0)
                    {
                        adminCards += $"<div class=\"card-group\">";
                    }
                    adminCards += $"<div class=\"card col-4 thumbnail\">" +
                                  $"<img " +
                                  $"class=\"card-image-top img-fluid img-thumbnail\"" +
                                  $"onerror=\"this.src=\'https://i.ytimg.com/vi/{game.Trailer}/maxresdefault.jpg\';\"" +
                                  $"src=\"{game.ImageThumbnail}\">" +
                                  $"<div class=\"card-body\">" +
                                  $"<h4 class=\"card-title\">{game.Title}</h4>" +
                                  $"<p class=\"card-text\"><strong>Price</strong> - {game.Price}&euro;</p>" +
                                  $"<p class=\"card-text\"><strong>Size</strong> - {game.Size} GB</p>" +
                                  $"<p class=\"card-text\">{description}</p>" +
                                  $"</div>" +
                                  $"<div class=\"card-footer\">" +
                                  $"<a class=\"card-button btn btn-warning\" name=\"edit\" href=\"/edit/{game.Id}\">Edit</a>" +
                                  $"<a class=\"card-button btn btn-danger\" name=\"delete\" href=\"/delete/{game.Id}\">Delete</a>" +
                                  $"<a class=\"card-button btn btn-outline-primary\" name=\"info\" href=\"/info/{game.Id}\">Info</a>" +
                                  $"<a class=\"card-button btn btn-primary\" name=\"buy\" href=\"/buy/{game.Id}\">Buy</a>" +
                                  $"</div>" +
                                  $"</div>";
                    if (counter == counter + 3)
                    {
                        adminCards += "</div>";
                    }
                    counter++;
                }
                this.ViewData["carts"] = adminCards;
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

            return this.FileViewResponse(@"home\index");
        }

        public IHttpResponse GetOwnedGames(IHttpRequest req)
        {
            string cards = "";
            var counter = 0;


            User user = null;
            try
            {
                var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
                user = Context.Users.FirstOrDefault(u => u.Id == currentUserId);
                var orders = Context.GameOrders.Where(o => o.Order.UserId == currentUserId);
                var games = orders.Select(o => o.Game);
                foreach (var game in games)
                {
                    var description = game.Description;
                    if (description.Length > 300)
                    {
                        description = description.Substring(0, 300);
                    }
                    if (counter % 3 == 0)
                    {
                        cards += $"<div class=\"card-group\">";
                    }
                    cards += $"<div class=\"card col-4 thumbnail\">" +
                             $"<img " +
                             $"class=\"card-image-top img-fluid img-thumbnail\"" +
                             $"onerror=\"this.src=\'https://i.ytimg.com/vi/{game.Trailer}/maxresdefault.jpg\';\"" +
                             $"src=\"{game.ImageThumbnail}\">" +
                             $"<div class=\"card-body\">" +
                             $"<h4 class=\"card-title\">{game.Title}</h4>" +
                             $"<p class=\"card-text\"><strong>Price</strong> - {game.Price}&euro;</p>" +
                             $"<p class=\"card-text\"><strong>Size</strong> - {game.Size} GB</p>" +
                             $"<p class=\"card-text\">{description}</p>" +
                             $"</div>" +
                             $"<div class=\"card-footer\">" +
                             $"<a class=\"card-button btn btn-outline-primary\" name=\"info\" href=\"/info/{game.Id}\">Info</a>" +
                             $"<a class=\"card-button btn btn-primary\" name=\"buy\" href=\"/buy/{game.Id}\">Buy</a>" +
                             $"</div>" +
                             $"</div>";

                    if (counter == counter + 3)
                    {
                        cards += "</div>";
                    }
                    counter++;

                }
                counter = 0;
                this.ViewData["carts"] = cards;
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
                var orders = Context.GameOrders.Where(o => o.Order.UserId == user.Id);
                var games = orders.Select(o => o.Game);
                string adminCards = "";
                foreach (var game in games)
                {
                    var description = game.Description;
                    if (description.Length > 300)
                    {
                        description = description.Substring(0, 300);
                    }
                    if (counter % 3 == 0)
                    {
                        adminCards += $"<div class=\"card-group\">";
                    }
                    adminCards += $"<div class=\"card col-4 thumbnail\">" +
                                  $"<img " +
                                  $"class=\"card-image-top img-fluid img-thumbnail\"" +
                                  $"onerror=\"this.src=\'https://i.ytimg.com/vi/{game.Trailer}/maxresdefault.jpg\';\"" +
                                  $"src=\"{game.ImageThumbnail}\">" +
                                  $"<div class=\"card-body\">" +
                                  $"<h4 class=\"card-title\">{game.Title}</h4>" +
                                  $"<p class=\"card-text\"><strong>Price</strong> - {game.Price}&euro;</p>" +
                                  $"<p class=\"card-text\"><strong>Size</strong> - {game.Size} GB</p>" +
                                  $"<p class=\"card-text\">{description}</p>" +
                                  $"</div>" +
                                  $"<div class=\"card-footer\">" +
                                  $"<a class=\"card-button btn btn-warning\" name=\"edit\" href=\"/edit/{game.Id}\">Edit</a>" +
                                  $"<a class=\"card-button btn btn-danger\" name=\"delete\" href=\"/delete/{game.Id}\">Delete</a>" +
                                  $"<a class=\"card-button btn btn-outline-primary\" name=\"info\" href=\"/info/{game.Id}\">Info</a>" +
                                  $"<a class=\"card-button btn btn-primary\" name=\"buy\" href=\"/buy/{game.Id}\">Buy</a>" +
                                  $"</div>" +
                                  $"</div>";
                    if (counter == counter + 3)
                    {
                        adminCards += "</div>";
                    }
                    counter++;
                }
                this.ViewData["carts"] = adminCards;
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

            return this.FileViewResponse(@"home\index");
        }
    }
}