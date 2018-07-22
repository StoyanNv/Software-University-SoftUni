namespace BookLibrary.Web.Pages.Books
{
    using Data;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    public class DetailsModel : BaseModel
    {
        public DetailsModel(BookLibraryContext context) : base(context)
        {
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }

        public void OnPostReturnBook(int id)
        {
            var book = this.Context.Books.FirstOrDefault(b => b.Id == id);
            var borrowedBook = this.Context.BorrowedBooks.FirstOrDefault(b => b.Book == book);
            this.Context.BorrowedBooks.Remove(borrowedBook);
            this.Context.SaveChanges();
            this.OnGet(id);
        }

        public IActionResult OnGet(int id)
        {
            var book = this.Context
                .Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return this.NotFound();
            }

            if (this.Context.BorrowedBooks.Select(b => b.BookId).Contains(book.Id))
            {
                IsBorrowed = true;
            }
            else
            {
                IsBorrowed = false;
            }
            this.Id = book.Id;
            this.Title = book.Title;
            this.Description = book.Description;
            this.ImageUrl = book.CoverImage;
            this.Author = book.Author.Name;

            return this.Page();
        }
    }
}