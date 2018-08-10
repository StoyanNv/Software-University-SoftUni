namespace BookLibrary.Web.Controllers
{
    using BookLibrary.Models;
    using Data;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using Models.BindingModels;
    using Models.ViewModels;
    using System;
    using System.Linq;
    public class MoviesController : BaseController
    {
        public MoviesController(BookLibraryContext context) : base(context) { }
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return this.View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Add(MovieBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            var director = GetOrCreateDirector(model);
            var movie = CreateMovie(model, director);


            return RedirectToAction("Details", new { id = movie.Id });
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var movie = this
                .Context
                .Movies
                .Include(m => m.Director)
                .FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return this.NotFound();
            }

            var model = new[] { movie }.Select(MovieDetailsViewModel.FormMovie).First();
            if (this.Context.BorrowedMovies.Select(b => b.MovieId).Contains(movie.Id))
            {
                model.IsBorrowed = true;
            }
            else
            {
                model.IsBorrowed = false;
            }
            return this.View(model);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Borrow(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var movie = this.Context.Movies.Find(id);
            if (movie == null)
            {
                return this.NotFound();
            }

            var model = new BorrowBindingModel()
            {
                Borrowers = this.Context.Borrowers
                    .Select(b => new SelectListItem()
                    {
                        Text = b.Name,
                        Value = b.Id.ToString()
                    })
                    .ToList()
            };
            return this.View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Borrow(BorrowBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }

            // TODO: If the book has been borrowed for the current period, return an error message
            var borrower = this.Context.Borrowers.Find(model.BorrowerId);
            int movieId = Convert.ToInt32(this.RouteData.Values["id"]);
            var movie = this.Context.Movies.Find(movieId);
            if (borrower == null || movie == null)
            {
                // TODO: ModelState.AddModelError()
                return this.View();
            }

            if (model.StartDate > model.EndDate && model.EndDate != null)
            {
                return this.View();
            }

            var borrowedMovie = new BorrowersMovies()
            {
                MovieId = movie.Id,
                BorrowerId = borrower.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            this.Context.BorrowedMovies.Add(borrowedMovie);
            this.Context.SaveChanges();
            return RedirectToAction("Details", new { id = movie.Id });
        }
        [HttpPost]
        [Authorize]
        public IActionResult Return(int id)
        {
            var movie = this.Context.Movies.FirstOrDefault(m => m.Id == id);
            var borrowedMovies = this.Context.BorrowedMovies.FirstOrDefault(m => m.Movie == movie);
            this.Context.BorrowedMovies.Remove(borrowedMovies);
            this.Context.SaveChanges();
            return RedirectToAction("Details", new { id });
        }
        private Director GetOrCreateDirector(MovieBindingModel model)
        {
            var director = this.Context.Directors.FirstOrDefault(d => d.Name == model.Director);
            if (director == null)
            {
                director = new Director()
                {
                    Name = model.Director
                };
                this.Context.Directors.Add(director);
                this.Context.SaveChanges();
            }

            return director;
        }
        private Movie CreateMovie(MovieBindingModel model, Director director)
        {
            var movie = new Movie()
            {
                Title = model.Title,
                Description = model.Description,
                PosterImage = model.ImageUrl,
                DirectorId = director.Id
            };
            this.Context.Movies.Add(movie);
            this.Context.SaveChanges();
            return movie;
        }
    }
}