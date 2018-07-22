namespace BookLibrary.Web.Models
{
    using Data;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class BaseModel : PageModel
    {
        public BaseModel(BookLibraryContext context)
        {
            this.Context = context;
        }

        public BookLibraryContext Context { get; set; }
    }
}