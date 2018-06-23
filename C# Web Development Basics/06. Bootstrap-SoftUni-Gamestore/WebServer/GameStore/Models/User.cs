using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HTTPServer.GameStore.Models
{
    public class User
    {
        public User()
        {
            this.Orders = new List<Order>();
        }
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FullName { get; set; }
        public bool Administrator { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}