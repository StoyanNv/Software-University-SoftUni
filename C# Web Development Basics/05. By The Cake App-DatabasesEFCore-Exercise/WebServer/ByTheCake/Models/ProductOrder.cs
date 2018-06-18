namespace HTTPServer.ByTheCake.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductOrder
    {
        [Key]
        public int OrderId { get; set; }

        public Order Order { get; set; }

        [Key]
        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}