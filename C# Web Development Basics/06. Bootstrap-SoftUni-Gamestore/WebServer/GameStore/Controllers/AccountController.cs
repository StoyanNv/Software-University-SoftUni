namespace HTTPServer.GameStore.Controllers
{
    using Infrastructure;
    using Models;
    using Server.Http;
    using Server.Http.Contracts;
    using Server.Http.Response;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class AccountController : Controller
    {
        public IHttpResponse Login()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["guestHeader"] = "block";
            this.ViewData["userHeader"] = "none";
            this.ViewData["adminHeader"] = "none";

            return this.FileViewResponse(@"account\login");
        }

        public IHttpResponse Login(IHttpRequest req)
        {
            const string formEmailKey = "email";
            const string formPasswordKey = "password";
            const string emptyFieldsMessage = "You have empty fields.";
            var error = File.ReadAllText(@"..\..\..\GameStore\Resources\Utilities\error.html");

            if (!req.FormData.ContainsKey(formEmailKey)
                || !req.FormData.ContainsKey(formPasswordKey))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                this.ViewData["errorMessage"] = emptyFieldsMessage;
                return this.FileViewResponse(@"account\login");
            }

            var email = req.FormData[formEmailKey];
            var password = req.FormData[formPasswordKey];

            if (string.IsNullOrWhiteSpace(email)
                || string.IsNullOrWhiteSpace(password))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                this.ViewData["errorMessage"] = emptyFieldsMessage;
                return this.FileViewResponse(@"account\login");
            }
            User dbUser = null;
            using (this.Context)
            {
                dbUser = this.Context.Users.FirstOrDefault(u => u.Email == email);
            }
            if (dbUser == null)
            {
                return this.RejectLoginAttempt();
            }
            if (password != dbUser.Password)
            {
                return this.RejectLoginAttempt();
            }
            return CompleteLogin(req, dbUser.Id);
        }
        public IHttpResponse Register()
        {
            this.ViewData["showError"] = "none";
            this.ViewData["guestHeader"] = "block";
            this.ViewData["userHeader"] = "none";
            this.ViewData["adminHeader"] = "none";
            return this.FileViewResponse(@"account\register");
        }
        public IHttpResponse Register(IHttpRequest req)
        {
            const string formNameKey = "fullName";
            const string formEmail = "email";
            const string formPasswordKey = "password";
            const string formConfirmPasswordKey = "confirm-password";

            const string invalidPasswordMessage = "Invalid Password. It should be at least 6 symbols long, containing 1 uppercase letter, 1 lowercase letter and 1 digit.";
            const string passwordsMatchMessage = "Passwords don't match.";
            const string invalidEmailMessage = "Email – must contain @ sign and a period, or already exists.";
            const string invalidNameMessage = "Name contains unacceptable symbols.";
            const string emptyFieldsMessage = "You have empty fields.";

            var error = File.ReadAllText(@"..\..\..\GameStore\Resources\Utilities\error.html");

            var namePattern = "^[a-zA-Z -.]+$";
            var nameRegex = new Regex(namePattern);
            var passwordPattern = @"(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{6,}";
            var passwordRegex = new Regex(passwordPattern);

            if (!req.FormData.ContainsKey(formNameKey)
                || !req.FormData.ContainsKey(formPasswordKey)
                || !req.FormData.ContainsKey(formPasswordKey)
                || !req.FormData.ContainsKey(formConfirmPasswordKey))

            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                this.ViewData["errorMessage"] = emptyFieldsMessage;
                return this.FileViewResponse(@"account\register");
            }
            bool admin = false;
            var name = req.FormData[formNameKey];
            var email = req.FormData[formEmail];
            var password = req.FormData[formPasswordKey];
            var confirmPassword = req.FormData[formConfirmPasswordKey];


            if (this.Context.Users.Select(u => u.Email).Contains(email) || !email.Contains('@') || !email.Contains('.'))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                this.ViewData["errorMessage"] = invalidEmailMessage;
                return this.FileViewResponse(@"account\register");
            }
            if (!nameRegex.IsMatch(name))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                this.ViewData["errorMessage"] = invalidNameMessage;
                return this.FileViewResponse(@"account\register");
            }
            if (!passwordRegex.IsMatch(password))
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                this.ViewData["errorMessage"] = invalidPasswordMessage;
                return this.FileViewResponse(@"account\register");
            }
            if (password != confirmPassword)
            {
                this.ViewData["error"] = error;
                this.ViewData["showError"] = "block";
                this.ViewData["guestHeader"] = "block";
                this.ViewData["userHeader"] = "none";
                this.ViewData["adminHeader"] = "none";
                this.ViewData["errorMessage"] = passwordsMatchMessage;
                return this.FileViewResponse(@"account\register");
            }

            var user = new User()
            {
                FullName = name,
                Administrator = admin,
                Email = email,
                Password = password
            };
            using (this.Context)
            {
                this.Context.Users.Add(user);
                this.Context.SaveChanges();
                var firstUser = Context.Users.FirstOrDefault(x => x.Id == 1);
                if (firstUser != null)
                {
                    firstUser.Administrator = true;
                }
                this.Context.SaveChanges();
            }

            return new RedirectResponse("/login");
        }
        public IHttpResponse Logout(IHttpRequest req)
        {
            req.Session.Clear();

            return new RedirectResponse("/login");
        }
        private static IHttpResponse CompleteLogin(IHttpRequest req, int userId)
        {
            req.Session.Add(SessionStore.CurrentUserKey, userId);
            req.Session.Add(ShoppingCart.SessionKey, new ShoppingCart());

            return new RedirectResponse("/");
        }

        private IHttpResponse RejectLoginAttempt()
        {
            const string noSuchUserMessage = "There is no such user.";

            var error = File.ReadAllText(@"..\..\..\GameStore\Resources\Utilities\error.html");
            this.ViewData["error"] = error;
            this.ViewData["showError"] = "block";
            this.ViewData["guestHeader"] = "block";
            this.ViewData["userHeader"] = "none";
            this.ViewData["adminHeader"] = "none";
            this.ViewData["errorMessage"] = noSuchUserMessage;
            return this.FileViewResponse(@"account\login");
        }
    }
}
