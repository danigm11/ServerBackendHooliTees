namespace ServerBackendHooliTees.Models.Database.Entities;
using Microsoft.EntityFrameworkCore;

[Index(nameof(Hash), IsUnique = true)]
public class Orders
{
    public int Id { get; set; }
    public decimal PriceETH { get; set; }
    public string Hash { get; set; }
    public string ClientWallet { get; set; }
    public decimal TotalPrice { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }

    public int UsersId { get; set; }
    public int ProductOrderId { get; set; }

    //  Foreign Key
    public ICollection<Users> Users { get; set; }
    public ICollection<ProductOrder> ProductOrder { get; set; }
}
