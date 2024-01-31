namespace ServerBackendHooliTees.Database.Entities;

public class ShoppingCart
{

    public int Id { get; set; }
    public int UserId { get; set; }

    // Foreign Keys
    public ICollection<ShoppingCart> ShoppingCartId { get; set; }
}
