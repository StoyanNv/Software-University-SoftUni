namespace BookLibrary.Web.Pages.Borrowers
{
    using BookLibrary.Models;
    using Data;
    using Models;
    using Microsoft.AspNetCore.Mvc;

    public class AddModel : BaseModel
    {
        public AddModel(BookLibraryContext context) : base(context)
        {
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Address { get; set; }

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