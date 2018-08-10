namespace BookLibrary.Web.Models.ViewModels
{
    using BookLibrary.Models;
    using System;

    public class MovieDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public string PosterImage { get; set; }

        public bool IsBorrowed { get; set; }

        public static Func<Movie, MovieDetailsViewModel> FormMovie
        {

            get
            {
                return movie => new MovieDetailsViewModel()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    Description = movie.Description,
                    Director = movie.Director.Name,
                    PosterImage = movie.PosterImage

                };
            }

        }
    }
}