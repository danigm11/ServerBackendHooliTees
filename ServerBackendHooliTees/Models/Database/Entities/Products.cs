using Microsoft.EntityFrameworkCore;

namespace ServerBackendHooliTees.Models.Database.Entities;

[Index(nameof(Id), IsUnique = true)]
public class Products
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public string Image { get; set; }

    public int CartProductId { get; set; }
    public int ProductOrderId { get; set; }

    //  Foreign Keys
    public ICollection<CartProducts> CartProduct { get; set; }
    public ICollection<ProductOrder> ProductOrder { get; set; }

}
