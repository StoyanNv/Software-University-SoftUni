namespace BookLibrary.Web.Models
{
    using BookLibrary.Models;
    using System;
    public class BookConciseViewModel
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }

        public static Func<Book, BookConciseViewModel> FromBook
        {
            get
            {
                return book => new BookConciseViewModel()
                {
                    BookId = book.Id,
                    Title = book.Title,
                    AuthorId = book.Author.Id,
                    Author = book.Author.Name,
                };
            }
        }
    }
}