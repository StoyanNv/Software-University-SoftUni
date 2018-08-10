namespace BookLibrary.Web.Pages
{
    using Data;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;

    public class IndexModel : BaseModel
    {
        public IndexModel(BookLibraryContext context) : base(context)
        {
        }

        public IEnumerable<BookConciseViewModel> Books { get; set; }

        public IEnumerable<MoviesConciseViewModel> Movies { get; set; }

        public string LogedUser
        {
            get
            {
                if (HttpContext.Session.GetString("Username") != null)
                {
                    return HttpContext.Session.GetString("Username");
                }
                return HttpContext.Session.GetString("Username");
            }
            set { this.LogedUser = value; }
        }

        public void OnGet()
        {
            {

                this.Books = this.Context.Books
                    .Include(b => b.Author)
                    .OrderBy(b => b.Title)
                    .Select(BookConciseViewModel.FromBook)
                    .ToList();
                this.Movies = this.Context.Movies
                    .Include(b => b.Director)
                    .OrderBy(b => b.Title)
                    .Select(MoviesConciseViewModel.FromMovie)
                    .ToList();

                foreach (var book in this.Books)
                {
                    if (this.Context.BorrowedBooks.Select(b => b.BookId).Contains(book.BookId))
                    {
                        book.IsBorrowed = true;
                    }
                    else
                    {
                        book.IsBorrowed = false;
                    }
                }

                foreach (var movie in this.Movies)
                {
                    if (this.Context.BorrowedMovies.Select(m => m.MovieId).Contains(movie.MovieId))
                    {
                        movie.IsBorrowed = true;
                    }
                    else
                    {
                        movie.IsBorrowed = false;
                    }
                }
            }
        }
    }
}