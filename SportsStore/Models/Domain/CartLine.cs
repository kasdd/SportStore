namespace SportsStore.Models.Domain
{
    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get { return Product.Price * Quantity; } }
    }
}