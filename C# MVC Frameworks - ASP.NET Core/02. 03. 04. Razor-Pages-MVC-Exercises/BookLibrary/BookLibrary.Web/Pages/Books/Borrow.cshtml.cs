namespace BookLibrary.Web.Pages.Books
{
    using BookLibrary.Models;
    using Data;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Models;
    using Models.BindingModels;
    using System;
    using System.Linq;

    [Authorize]
    public class BorrowModel : BaseModel
    {
        public BorrowModel(BookLibraryContext context) : base(context)
        {
            this.BorrowingForm = new BorrowBindingModel();      
        }
        [BindProperty]
        public BorrowBindingModel BorrowingForm { get; set; }

        public void OnGet()
        {
            this.BorrowingForm.Borrowers = this.Context.Borrowers
                .Select(b => new SelectListItem()
                {
                    Text = b.Name,
                    Value = b.Id.ToString()
                })
                .ToList();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // TODO: If the book has been borrowed for the current period, return an error message
            var borrower = this.Context.Borrowers.Find(this.BorrowingForm.BorrowerId);
            int bookId = Convert.ToInt32(this.RouteData.Values["id"]);
            var book = this.Context.Books.Find(bookId);
            if (borrower == null || book == null)
            {
                // TODO: ModelState.AddModelError()
                return Page();
            }

            if (BorrowingForm.StartDate > BorrowingForm.EndDate && BorrowingForm.EndDate != null)
            {
                return Page();
            }

            var borrowedBook = new BorrowersBooks()
            {
                BookId = book.Id,
                BorrowerId = borrower.Id,
                StartDate = BorrowingForm.StartDate,
                EndDate = BorrowingForm.EndDate
            };

            this.Context.BorrowedBooks.Add(borrowedBook);
            this.Context.SaveChanges();
            return this.RedirectToPage("/Books/Details", new { id = book.Id });
        }
    }
}