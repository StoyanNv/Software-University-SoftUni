namespace BookLibrary.Web.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class LogInBindingModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(5)]
        public string Password { get; set; }
    }
}