namespace BookLibrary.Web.Pages.Borrowers
{
    using BookLibrary.Models;
    using Data;
    using Filters;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using System.ComponentModel.DataAnnotations;

    [Authorize]
    public class AddModel : BaseModel
    {
        public AddModel(BookLibraryContext context) : base(context)
        {
        }
        [BindProperty]
        [Required, MinLength(3, ErrorMessage = "The Name must be at least three sybols long.")]
        public string Name { get; set; }
        [BindProperty]
        [Required, MinLength(6, ErrorMessage = "The Address must be at least six sybols long.")]
        public string Address { get; set; }

        public void OnGet() { }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Borrower borrower = new Borrower()
            {
                Addres = this.Address,
                Name = this.Name
            };
            this.Context.Borrowers.Add(borrower);
            Context.SaveChanges();
            return RedirectToPage("/Index");
        }
    }
}