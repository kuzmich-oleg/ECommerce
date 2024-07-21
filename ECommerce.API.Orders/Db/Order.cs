namespace ECommerce.API.Orders.Db
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public decimal Total { get; set; }

        public ICollection<OrderItem>? Items { get; set; }
    }
}
