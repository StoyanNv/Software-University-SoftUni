namespace BookLibrary.Web.Models.ViewModels
{
    using BookLibrary.Models;
    using System;

    public class MoviesConciseViewModel
    {
        public int MovieId { get; set; }

        public string Title { get; set; }

        public int DirectorId { get; set; }

        public string Director { get; set; }

        public bool IsBorrowed { get; set; }

        public static Func<Movie, MoviesConciseViewModel> FromMovie
        {
            get
            {
                return movie => new MoviesConciseViewModel()
                {
                    MovieId = movie.Id,
                    Title = movie.Title,
                    DirectorId = movie.Director.Id,
                    Director = movie.Director.Name,
                };
            }
        }
    }
}