namespace BookLibrary.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;

    public class BaseController : Controller
    {
        public BaseController(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }
    }
}