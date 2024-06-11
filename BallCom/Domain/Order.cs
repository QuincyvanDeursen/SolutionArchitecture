namespace Domain
{
    public class Order
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int CustomerId { get; set; }

        public IList<Product>? Products { get; set; }

        public Customer? Customer { get; set; }
    }
}
