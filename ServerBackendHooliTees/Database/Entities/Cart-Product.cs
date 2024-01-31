namespace ServerBackendHooliTees.Database.Entities
{
    public class Cart_Product
    {
        public ICollection<ShoppingCart> ShoppingCarts {  get; set; }
        public ICollection<Product> Products { get; set;}
        public int Quantity { get; set; }
    }
}
