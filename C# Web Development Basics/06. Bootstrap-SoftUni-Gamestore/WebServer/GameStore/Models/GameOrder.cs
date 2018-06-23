using System.ComponentModel.DataAnnotations;

namespace HTTPServer.GameStore.Models
{
    public class GameOrder
    {
        [Key]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Key]
        public int GameId { get; set; }

        public Game Game { get; set; }
    }
}