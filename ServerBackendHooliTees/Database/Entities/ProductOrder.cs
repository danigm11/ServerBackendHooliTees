namespace ServerBackendHooliTees.Database.Entities;
using Microsoft.EntityFrameworkCore;

public class ProductOrder
{
    public int Id { get; set; }
    public ICollection<Orders> Orders { get; set; }
    public ICollection<Product> Products { get; set; }
    public int Quantity { get; set; }
}
