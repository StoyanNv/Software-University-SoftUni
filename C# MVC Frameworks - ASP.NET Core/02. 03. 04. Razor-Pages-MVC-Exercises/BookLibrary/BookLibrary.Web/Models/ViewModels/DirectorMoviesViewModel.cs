namespace BookLibrary.Web.Models.ViewModels
{
    using BookLibrary.Models;
    using System;

    public class DirectorMoviesViewModel
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public static Func<Movie, DirectorMoviesViewModel> FromMovie
        {
            get
            {
                return movie => new DirectorMoviesViewModel()
                {
                    MovieId = movie.Id,
                    Title = movie.Title,
                };
            }
        }
    }
}