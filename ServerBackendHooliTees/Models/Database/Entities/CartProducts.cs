namespace ServerBackendHooliTees.Models.Database.Entities;

public class CartProducts
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int ShoppingCartId { get; set; }
    public int Quantity { get; set; }

    // Foreign Keys
    public ShoppingCart ShoppingCart { get; set; }
    public Products Product { get; set; }
}

