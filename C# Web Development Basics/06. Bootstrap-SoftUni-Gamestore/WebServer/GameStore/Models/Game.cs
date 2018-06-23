using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HTTPServer.GameStore.Models
{
    public class Game
    {
        public Game()
        {
            this.Orders = new List<GameOrder>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Trailer { get; set; }
        [Required]
        public string ImageThumbnail { get; set; }
        [Required]
        public double Size { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public ICollection<GameOrder> Orders { get; set; }
    }
}