namespace HTTPServer.ByTheCake.Controllers
{
    using Infrastructure;
    using Server.Http;
    using Server.Http.Contracts;
    using System.Linq;

    public class HomeController : Controller
    {
        public IHttpResponse Index() => this.FileViewResponse(@"home\index");

        public IHttpResponse About(IHttpRequest req)
        {
            var currentUserId = req.Session.Get<int>(SessionStore.CurrentUserKey);

            this.ViewData["Your Name"] = this.Context.Users.FirstOrDefault(u => u.Id == currentUserId).Name;

            return this.FileViewResponse(@"home\about");
        }
    }
}