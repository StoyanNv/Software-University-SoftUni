namespace HTTPServer.ByTheCake.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Orders = new List<Order>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(2048, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(2048, MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public DateTime RegistrationDate { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}