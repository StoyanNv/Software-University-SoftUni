namespace BookLibrary.Web.Controllers
{
    using Data;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    public class DirectorsController : BaseController
    {
        public DirectorsController(BookLibraryContext context) : base(context)
        {
            this.Movies = new List<DirectorMoviesViewModel>();
        }
        public IEnumerable<DirectorMoviesViewModel> Movies { get; set; }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var director = this.Context.Directors
                .Include(d => d.Movies)
                .FirstOrDefault(d => d.Id == id);
            if (director == null)
            {
                return this.NotFound();
            }
            this.Movies = director.Movies.Select(DirectorMoviesViewModel.FromMovie);
            var model = new DirectorDetailsViewModel()
            {
                Movies = this.Movies,
                Name = director.Name
            };
            return View(model);
        }
    }
}