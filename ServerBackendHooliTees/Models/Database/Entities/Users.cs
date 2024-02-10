using Microsoft.EntityFrameworkCore;
namespace ServerBackendHooliTees.Models.Database.Entities;

[Index(nameof(Email), IsUnique = true)]
public class Users
{

    public long Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public bool IsAdmin { get; set; }

    public int OrdersId { get; set; }

    //  Foreng Keys
    public ICollection<Orders> Orders { get; set; }


}
