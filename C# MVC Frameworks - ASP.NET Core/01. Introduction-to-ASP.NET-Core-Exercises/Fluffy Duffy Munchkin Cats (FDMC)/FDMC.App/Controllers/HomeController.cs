namespace FDMC.App.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Models.ViewModels;
    using System.Linq;

    public class HomeController : Controller
    {
        public HomeController(CatContext context)
        {
            this.Context = context;
        }

        public CatContext Context { get; private set; }

        public IActionResult Index()
        {
            var cats = this.Context
                .Cats
                .Select(c => new CatConciseViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToArray();
            return View(cats);
        }
    }
}