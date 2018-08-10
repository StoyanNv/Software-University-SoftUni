namespace BookLibrary.Web.Controllers
{
    using Common;
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Models.BindingModels;
    using System.Linq;

    public class UsersController : BaseController
    {
        public UsersController(BookLibraryContext context) : base(context) { }
        [HttpGet]
        public IActionResult LogIn(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                TempData["Error"] = message;
            }
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LogInBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            var user = Context.Users.FirstOrDefault(u => u.Username == model.Username);
            if (user != null && user.PasswordHash == PasswordUtilities.GetPasswordHash(model.Password))
            {
                HttpContext.Session.SetString("Username", model.Username);
                return RedirectToPage("/Index");
            }

            return this.View();
        }
        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}