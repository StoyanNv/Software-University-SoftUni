namespace FDMC.App.Controllers
{
    using FDMC.App.Models.BindingModels;
    using FDMC.App.Models.ViewModels;
    using FDMC.Data;
    using FDMC.Models;
    using Microsoft.AspNetCore.Mvc;

    public class CatsController : Controller
    {
        public CatsController(CatContext context)
        {
            this.Context = context;
        }

        public CatContext Context { get; private set; }

        public IActionResult Details(int id)
        {
            var cat = this.Context.Cats.Find(id);
            var catModel = new CatDetailsViewModel()
            {
                Name = cat.Name,
                Age = cat.Age,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };
            if (catModel == null)
            {
                return NotFound();
            }
            return View(catModel);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(CatCreatingBindingModel model)
        {
            Cat cat = new Cat()
            {
                Name = model.Name,
                Age = model.Age,
                Breed = model.Breed,
                ImageUrl = model.ImageUrl
            };
            Context.Cats.Add(cat);
            this.Context.SaveChanges();
            return RedirectToAction("Details", new { id = cat.Id });
        }
    }
}