namespace ServerBackendHooliTees.Database.Entities;

public class ShoppingCart
{

    public int Id { get; set; }
    public int UserId { get; set; }
    public Users User { get; set; }

}
