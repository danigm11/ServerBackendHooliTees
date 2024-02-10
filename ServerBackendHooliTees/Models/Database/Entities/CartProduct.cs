namespace ServerBackendHooliTees.Models.Database.Entities;

public class CartProduct
{
    public int Id { get; set; }
    public int ProductsId { get; set; }
    public int ShoppingCartId { get; set; }
    public int Quantity { get; set; }

    // Foreign Keys
    public ICollection<ShoppingCart> ShoppingCart { get; set; }
    public ICollection<Products> Products { get; set; }

}
