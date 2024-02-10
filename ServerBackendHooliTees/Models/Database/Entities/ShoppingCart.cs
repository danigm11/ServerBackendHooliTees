namespace ServerBackendHooliTees.Models.Database.Entities;

public class ShoppingCart
{

    public int Id { get; set; }
    public int UserId { get; set; }
    public int CartProductId { get; set; }


    // Foreign Keys
    public Users User { get; set; }
    public ICollection<CartProduct> CartProduct { get; set; }
}
