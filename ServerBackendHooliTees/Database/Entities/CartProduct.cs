namespace ServerBackendHooliTees.Database.Entities;

public class CartProduct
{
    public int CartProductId { get; set; }
    public ICollection<ShoppingCart> ShoppingCart {  get; set; }
    public ICollection<Product> ProductsID { get; set;}
    public int Quantity { get; set; }
}
