namespace BookLibrary.Web.Pages.Books
{
    using BookLibrary.Models;
    using Data;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class AddModel : BaseModel
    {
        public AddModel(BookLibraryContext context)
            : base(context) { }

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Author { get; set; }

        [Display(Name = "Image URL")]
        [DataType(DataType.Url)]
        [BindProperty]
        public string ImageUrl { get; set; }

        [BindProperty]
        public string Description { get; set; }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newBook = this.AddBook();
            return this.RedirectToPage("/Books/Details", new { id = newBook.Id });
        }

        private Book AddBook()
        {
            var author = this.CreateOrUpdateAuthor();
            var book = new Book()
            {
                Title = this.Title,
                Description = this.Description,
                CoverImage = this.ImageUrl,
                AuthorId = author.Id
            };

            this.Context.Books.Add(book);
            this.Context.SaveChanges();
            return book;
        }

        private Author CreateOrUpdateAuthor()
        {
            var author = Context.Authors.FirstOrDefault(a => a.Name == this.Author);
            if (author == null)
            {
                author = new Author()
                {
                    Name = this.Author
                };
                this.Context.Authors.Add(author);
            }

            return author;
        }
    }
}