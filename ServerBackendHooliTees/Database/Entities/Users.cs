namespace ServerBackendHooliTees.Database.Entities;

public class Users
{

    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public Boolean IsAdmin { get; set; }

    //  Foreng Keys
    public ICollection<Orders> Orders { get; set; } 
    public ShoppingCart ShoppingCart { get; set; }

}
