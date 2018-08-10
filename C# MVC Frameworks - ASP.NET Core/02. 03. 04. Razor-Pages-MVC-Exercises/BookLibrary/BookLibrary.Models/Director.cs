using System.Collections.Generic;

namespace BookLibrary.Models
{
    public class Director
    {
        public Director()
        {
            Movies = new List<Movie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}