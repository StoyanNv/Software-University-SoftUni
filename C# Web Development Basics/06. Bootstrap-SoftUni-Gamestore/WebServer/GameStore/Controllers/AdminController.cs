using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using HTTPServer.GameStore.Infrastructure;
using HTTPServer.GameStore.Models;
using HTTPServer.Server.Http;
using HTTPServer.Server.Http.Contracts;
using HTTPServer.Server.Http.Response;

namespace HTTPServer.GameStore.Controllers
{
    public class AdminController : Controller
    {
        public IHttpResponse ShowGames(IHttpRequest req)
        {
            User user = null;
            try
            {
                var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
                user = Context.Users.FirstOrDefault(u => u.Id == currentUserId);
            }
            catch (Exception)
            {
                return this.FileViewResponse(@"\");
            }

            if (user.Administrator)
            {
                var html = "";

                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                using (Context)
                {
                    var games = Context.Games;
                    foreach (var game in games)
                    {
                        html += $"<tr>" +
                                $"<th scope = \"row\">1</th>" +
                                $"<td>{game.Title}</td><td>{game.Size} GB</td>" +
                                $"<td> {game.Price} &euro;</td>" +
                                $"<td>" +
                                $"<a href=\"/edit/{game.Id}\" class=\"btn btn-warning btn-sm\">Edit</a>" +
                                $"<a href = \"/delete/{game.Id}\" class=\"btn btn-danger btn-sm\">Delete</a>" +
                                $"</td>" +
                                $"</tr>";
                    }
                }
                this.ViewData["games"] = html;
                return this.FileViewResponse(@"admin\admin-games");
            }
            return new RedirectResponse(@"\");
        }

        public IHttpResponse AddGame(IHttpRequest req)
        {
            User user = null;
            try
            {
                var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
                user = Context.Users.FirstOrDefault(u => u.Id == currentUserId);
            }
            catch (Exception)
            {
                return this.FileViewResponse(@"\");
            }

            if (!user.Administrator)
            {
                return new RedirectResponse(@"\");
            }
            this.ViewData["guestHeader"] = "none";
            this.ViewData["userHeader"] = "none";
            this.ViewData["adminHeader"] = "block";
            this.ViewData["showError"] = "none";
            return this.FileViewResponse(@"admin\add-game");

        }
        public IHttpResponse AddGamePost(IHttpRequest req)
        {
            var error = File.ReadAllText(@"..\..\..\GameStore\Resources\Utilities\error.html");
            const string emptyFieldsMessage = "You have empty fields.";
            const string invalidTitleMessage = "Title – has to begin with uppercase letter and has length between 3 and 100 symbols (inclusive).";
            const string invalidPriceMessage = "Price –  must be a positive number.";
            const string invalidSizeMessage = "Size –  must be a positive number.";
            const string invalidThumbnailUrlMessage = "Thumbnail URL should start with http://, https://.";
            const string invalidDescriptionMessage = "Description – must be at least 20 symbols";

            const string formТitleKey = "title";
            const string formDescriptionKey = "description";
            const string formUrlKey = "url";
            const string formPriceKey = "price";
            const string formSizeKey = "size";
            const string formVideoUrlKey = "videoURL";
            const string formDateKey = "date";
            var titlePattern = "[A-Z].{3,100}";
            var titleRegex = new Regex(titlePattern);
            if (!req.FormData.ContainsKey(formТitleKey)
                || !req.FormData.ContainsKey(formDescriptionKey)
                || !req.FormData.ContainsKey(formUrlKey)
                || !req.FormData.ContainsKey(formPriceKey)
                || !req.FormData.ContainsKey(formVideoUrlKey)
                || !req.FormData.ContainsKey(formDateKey)
                || !req.FormData.ContainsKey(formSizeKey))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = emptyFieldsMessage;
                return this.FileViewResponse(@"admin\add-game");
            }

            var title = req.FormData[formТitleKey];
            var description = req.FormData[formDescriptionKey];
            var url = req.FormData[formUrlKey];
            var price = req.FormData[formPriceKey];
            var size = req.FormData[formSizeKey];
            var videoUrl = req.FormData[formVideoUrlKey];
            var date = req.FormData[formDateKey];

            if (string.IsNullOrWhiteSpace(title)
                || string.IsNullOrWhiteSpace(description)
                || string.IsNullOrWhiteSpace(price)
                || string.IsNullOrWhiteSpace(size)
                || string.IsNullOrWhiteSpace(videoUrl)
                || string.IsNullOrWhiteSpace(date))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = emptyFieldsMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            if (!titleRegex.IsMatch(title))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidTitleMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            if (decimal.Parse(price) <= 0)
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidPriceMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            if (double.Parse(size) <= 0)
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidSizeMessage;
                return this.FileViewResponse(@"admin\add-game");
            }

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidThumbnailUrlMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            if (description.Length < 20)
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidDescriptionMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            Game game = new Game()
            {
                Title = title,
                Description = description,
                ImageThumbnail = url,
                Trailer = videoUrl,
                Price = Math.Round(decimal.Parse(price), 2),
                Size = Math.Round(double.Parse(size), 1),
                ReleaseDate = DateTime.Parse(date)
            };
            using (Context)
            {
                Context.Games.Add(game);
                Context.SaveChanges();
            }
            return new RedirectResponse(@"\games");
        }

        public IHttpResponse Delete(IHttpRequest req, int id)
        {
            User user = null;
            try
            {
                var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
                user = Context.Users.FirstOrDefault(u => u.Id == currentUserId);
            }
            catch (Exception)
            {
                return this.FileViewResponse(@"\");
            }

            if (!user.Administrator)
            {
                return new RedirectResponse(@"\");
            }

            Game game = Context.Games.FirstOrDefault(g => g.Id == id);

            this.ViewData["title-del"] = game.Title;
            this.ViewData["description-del"] = game.Description;
            this.ViewData["thumbnail-del"] = game.ImageThumbnail;
            this.ViewData["price-del"] = $"{game.Price:f2}";
            this.ViewData["size-del"] = $"{game.Size:f1}";
            this.ViewData["video-del"] = game.Trailer;
            this.ViewData["date-del"] = game.ReleaseDate.ToString("yyyy-MM-dd");

            this.ViewData["guestHeader"] = "none";
            this.ViewData["userHeader"] = "none";
            this.ViewData["adminHeader"] = "block";
            return this.FileViewResponse(@"admin\delete-game");
        }

        public IHttpResponse DeletePost(int id)
        {
            using (Context)
            {
                var game = Context.Games.FirstOrDefault(g => g.Id == id);
                Context.Games.Remove(game);
                Context.SaveChanges();
            }
            return new RedirectResponse(@"\games");
        }

        public IHttpResponse Edit(IHttpRequest req, int id)
        {
            User user = null;
            try
            {
                var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
                user = Context.Users.FirstOrDefault(u => u.Id == currentUserId);
            }
            catch (Exception)
            {
                return this.FileViewResponse(@"\");
            }

            if (!user.Administrator)
            {
                return new RedirectResponse(@"\");
            }

            Game game = Context.Games.FirstOrDefault(g => g.Id == id);

            this.ViewData["title-edit"] = game.Title;
            this.ViewData["description-edit"] = game.Description;
            this.ViewData["thumbnail-edit"] = game.ImageThumbnail;
            this.ViewData["price-edit"] = $"{game.Price:f2}";
            this.ViewData["size-edit"] = $"{game.Size:f1}";
            this.ViewData["video-edit"] = game.Trailer;
            this.ViewData["date-edit"] = game.ReleaseDate.ToString("yyyy-MM-dd");

            this.ViewData["showError"] = "none";
            this.ViewData["guestHeader"] = "none";
            this.ViewData["userHeader"] = "none";
            this.ViewData["adminHeader"] = "block";
            return this.FileViewResponse(@"admin\edit-game");
        }

        public IHttpResponse EditPost(IHttpRequest req, int id)
        {
            var error = File.ReadAllText(@"..\..\..\GameStore\Resources\Utilities\error.html");
            const string emptyFieldsMessage = "You have empty fields.";
            const string invalidTitleMessage = "Title – has to begin with uppercase letter and has length between 3 and 100 symbols (inclusive).";
            const string invalidPriceMessage = "Price –  must be a positive number.";
            const string invalidSizeMessage = "Size –  must be a positive number.";
            const string invalidThumbnailUrlMessage = "Thumbnail URL should start with http://, https://.";
            const string invalidDescriptionMessage = "Description – must be at least 20 symbols";

            const string formТitleKey = "title";
            const string formDescriptionKey = "description";
            const string formUrlKey = "url";
            const string formPriceKey = "price";
            const string formSizeKey = "size";
            const string formVideoUrlKey = "videoURL";
            const string formDateKey = "date";
            var titlePattern = "[A-Z].{3,100}";
            var titleRegex = new Regex(titlePattern);
            if (!req.FormData.ContainsKey(formТitleKey)
                || !req.FormData.ContainsKey(formDescriptionKey)
                || !req.FormData.ContainsKey(formUrlKey)
                || !req.FormData.ContainsKey(formPriceKey)
                || !req.FormData.ContainsKey(formVideoUrlKey)
                || !req.FormData.ContainsKey(formDateKey)
                || !req.FormData.ContainsKey(formSizeKey))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = emptyFieldsMessage;
                return this.FileViewResponse(@"admin\add-game");
            }

            var title = req.FormData[formТitleKey];
            var description = req.FormData[formDescriptionKey];
            var url = req.FormData[formUrlKey];
            var price = req.FormData[formPriceKey];
            var size = req.FormData[formSizeKey];
            var videoUrl = req.FormData[formVideoUrlKey];
            var date = req.FormData[formDateKey];

            if (string.IsNullOrWhiteSpace(title)
                || string.IsNullOrWhiteSpace(description)
                || string.IsNullOrWhiteSpace(price)
                || string.IsNullOrWhiteSpace(size)
                || string.IsNullOrWhiteSpace(videoUrl)
                || string.IsNullOrWhiteSpace(date))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = emptyFieldsMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            if (!titleRegex.IsMatch(title))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidTitleMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            if (decimal.Parse(price) <= 0)
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidPriceMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            if (double.Parse(size) <= 0)
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidSizeMessage;
                return this.FileViewResponse(@"admin\add-game");
            }

            if (!url.StartsWith("http://") && !url.StartsWith("https://"))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidThumbnailUrlMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            if (description.Length < 20)
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "none";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "block";
                this.ViewData["errorMessage"] = invalidDescriptionMessage;
                return this.FileViewResponse(@"admin\add-game");
            }
            Game game = new Game()
            {
                Title = title,
                Description = description,
                ImageThumbnail = url,
                Trailer = videoUrl,
                Price = Math.Round(decimal.Parse(price), 2),
                Size = Math.Round(double.Parse(size), 1),
                ReleaseDate = DateTime.Parse(date)
            };
            using (Context)
            {
                var temp = Context.Games.Find(id);
                Context.Games.Remove(temp);
                Context.Games.Add(game);
                Context.SaveChanges();
            }
            return new RedirectResponse(@"\games");

        }
    }
}