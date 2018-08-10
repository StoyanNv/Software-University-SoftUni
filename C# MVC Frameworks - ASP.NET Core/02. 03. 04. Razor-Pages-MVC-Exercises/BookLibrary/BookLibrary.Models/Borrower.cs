namespace BookLibrary.Models
{
    using System.Collections.Generic;

    public class Borrower
    {
        public Borrower()
        {
            BorrowedBooks = new List<BorrowersBooks>();
            BorrowedMovies = new List<BorrowersMovies>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Addres { get; set; }
        public ICollection<BorrowersBooks> BorrowedBooks { get; set; }
        public ICollection<BorrowersMovies> BorrowedMovies{ get; set; }
    }
}