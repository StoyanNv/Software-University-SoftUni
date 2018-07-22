namespace BookLibrary.Web.Pages.Authors
{
    using Data;
    using Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Linq;

    public class DetailsModel : BaseModel
    {
        public DetailsModel(BookLibraryContext context) : base(context)
        {
        }

        public string Name { get; set; }
        public IEnumerable<AuthorBooksViewModel> BookNames { get; set; }

        public IActionResult OnGet(int id)
        {
            var author = this.Context
                .Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            this.Name = author.Name;
            this.BookNames = author.Books.Select(AuthorBooksViewModel.FromBook);
            return Page();
        }
    }
}