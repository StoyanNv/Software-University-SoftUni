namespace HTTPServer.ByTheCake.Controllers
{
    using Infrastructure;
    using Models;
    using Server.Common;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using Utilities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public class AccountController : Controller
    {
        public IHttpResponse Register()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"account\register");
        }

        public IHttpResponse Register(IHttpRequest req)
        {
            const string formNameKey = "name";
            const string formUsernameKey = "username";
            const string formPasswordKey = "password";
            const string formConfirmPasswordKey = "confirm-password";

            if (!req.FormData.ContainsKey(formNameKey)
                || !req.FormData.ContainsKey(formPasswordKey)
                || !req.FormData.ContainsKey(formPasswordKey)
                || !req.FormData.ContainsKey(formConfirmPasswordKey))

            {
                this.ViewData["error"] = "You have empty fields";
                this.ViewData["showError"] = "block";
                this.ViewData["authDisplay"] = "none";

                return this.FileViewResponse(@"account\register");
            }
            var name = req.FormData[formNameKey];
            var username = req.FormData[formUsernameKey];
            var password = req.FormData[formPasswordKey];
            var confirmPassword = req.FormData[formConfirmPasswordKey];
            try
            {
                CoreValidator.ThrowIfNotLongEnough(name, "name", 3);
                CoreValidator.ThrowIfNotLongEnough(username, "username", 3);
                CoreValidator.ThrowIfNullOrEmpty(password, "password");
                CoreValidator.ThrowIfNotEqualStrings(password, confirmPassword, "password", "confirmPassword");
            }
            catch (ArgumentException)
            {
                this.ViewData["error"] = "You have wrong fields";
                this.ViewData["showError"] = "block";
                this.ViewData["authDisplay"] = "none";

                return this.FileViewResponse(@"account\register");
            }
            var user = new User()
            {
                Name = req.FormData[formNameKey],
                Username = req.FormData[formUsernameKey],
                PasswordHash = PasswordUtilities.GenerateHash(password),
                RegistrationDate = DateTime.UtcNow,
                Orders = new List<Order>()
            };
            using (this.Context)
            {
                this.Context.Users.Add(user);
                this.Context.SaveChanges();
            }
            return CompleteLogin(req, user.Id);
        }

        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            const string formNameKey = "name";
            const string formPasswordKey = "password";

            if (!req.FormData.ContainsKey(formNameKey)
                || !req.FormData.ContainsKey(formPasswordKey))
            {
                this.ViewData["error"] = "You have empty fields";
                this.ViewData["showError"] = "block";
                this.ViewData["authDisplay"] = "none";

                return this.FileViewResponse(@"account\login");
            }

            var name = req.FormData[formNameKey];
            var password = req.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(name)
                || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["error"] = "You have empty fields";
                this.ViewData["showError"] = "block";
                this.ViewData["authDisplay"] = "none";

                return this.FileViewResponse(@"account\login");
            }
            User dbUser = null;
            using (this.Context)
            {
                dbUser = this.Context.Users.FirstOrDefault(u => u.Username == name);
            }
            if (dbUser == null)
            {
                return this.RejectLoginAttempt();
            }
            var passwordHash = PasswordUtilities.GenerateHash(password);
            if (passwordHash != dbUser.PasswordHash)
            {
                return this.RejectLoginAttempt();
            }
            return CompleteLogin(req, dbUser.Id);
        }

        public IHttpResponse Profile(IHttpRequest req)
        {
            var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);
            User currentUser = null;
            List<Order> orders = new List<Order>();
            using (this.Context)
            {
                orders = Context.Orders.Where(u => u.UserId == currentUserId).ToList();
                currentUser = this.Context.Users.Find(currentUserId);
            }
            this.ViewData["name"] = currentUser.Name;
            this.ViewData["registrationDate"] = currentUser.RegistrationDate.ToString("dd-MM-yyyy");
            this.ViewData["ordersCount"] = orders.Count.ToString();
            return this.FileViewResponse(@"account\profile");
        }

        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }

        private IHttpResponse RejectLoginAttempt()
        {
            this.ViewData["error"] = "There is no such user";
            this.ViewData["showError"] = "block";
            this.ViewData["authDisplay"] = "none";

            return this.FileViewResponse(@"account\login");
        }

        private static IHttpResponse CompleteLogin(IHttpRequest req, int userId)
        {
            req.Session.Add(SessionStore.CurrentUserKey, userId);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }
    }
}