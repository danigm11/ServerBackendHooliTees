namespace ServerBackendHooliTees.Database.Entities;

public class ShoppingCart
{

    public int Id { get; set; }
    public int UserId { get; set; }

    // Foreign Keys

    public Users Users { get; set; }
    public ICollection<CartProduct> CartProduct { get; set; }
}
