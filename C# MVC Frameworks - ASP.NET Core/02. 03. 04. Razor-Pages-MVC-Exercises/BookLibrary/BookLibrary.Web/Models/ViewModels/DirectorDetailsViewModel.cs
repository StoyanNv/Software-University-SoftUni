namespace BookLibrary.Web.Models.ViewModels
{
    using System.Collections.Generic;

    public class DirectorDetailsViewModel
    {
        public DirectorDetailsViewModel()
        {
            this.Movies = new List<DirectorMoviesViewModel>();
        }
        public string Name { get; set; }

        public IEnumerable<DirectorMoviesViewModel> Movies { get; set; }
    }
}