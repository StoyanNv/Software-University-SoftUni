using System.Collections.Generic;

namespace BookLibrary.Models
{
    public class Movie
    {
        public Movie()
        {
            Borrowers = new List<BorrowersMovies>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterImage { get; set; }
        public int DirectorId { get; set; }
        public Director Director { get; set; }
        public ICollection<BorrowersMovies> Borrowers { get; set; }
    }
}