namespace Order.Api.Models
{
    public class OrderItemModel
    {
        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public decimal SubTotal { get; set; }
    }
}
