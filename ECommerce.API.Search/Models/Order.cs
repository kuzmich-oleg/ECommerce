namespace ECommerce.API.Search.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal Total { get; set; }

        public List<OrderItem>? Items { get; set; }
    }
}
