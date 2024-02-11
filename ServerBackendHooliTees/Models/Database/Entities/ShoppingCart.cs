namespace ServerBackendHooliTees.Models.Database.Entities;

public class ShoppingCart
{

    public int Id { get; set; }
    public int UserId { get; set; }
    public int CartProductId { get; set; }


    // Foreign Keys
    public ICollection<CartProducts> CartProduct { get; set; }
}
