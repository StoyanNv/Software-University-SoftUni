using System.Collections.Generic;

namespace HTTPServer.GameStore.Models
{
    public class Order
    {
        public Order()
        {
            this.Games = new List<GameOrder>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public ICollection<GameOrder> Games { get; set; }
    }
}