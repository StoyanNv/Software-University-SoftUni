namespace BookLibrary.Web.Pages
{
    using Data;
    using Models;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;
    public class IndexModel : BaseModel
    {
        public IndexModel(BookLibraryContext context) : base(context)
        {
        }

        public IEnumerable<BookConciseViewModel> Books { get; set; }

        public void OnGet()
        {
            this.Books = this.Context.Books
                .Include(b => b.Author)
                .OrderBy(b => b.Title)
                .Select(BookConciseViewModel.FromBook)
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
        }
    }
}