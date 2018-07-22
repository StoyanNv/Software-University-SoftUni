namespace BookLibrary.Web.Models
{
    using BookLibrary.Models;
    using System;
    public class AuthorBooksViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        public static Func<Book, AuthorBooksViewModel> FromBook
        {
            get
            {
                return book => new AuthorBooksViewModel()
                {
                    BookId = book.Id,
                    Title = book.Title,
                };
            }
        }
    }
}