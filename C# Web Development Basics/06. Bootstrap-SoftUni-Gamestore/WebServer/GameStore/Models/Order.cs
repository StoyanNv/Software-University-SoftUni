namespace HTTPServer.GameStore.Models
{
    using System.Collections.Generic;

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