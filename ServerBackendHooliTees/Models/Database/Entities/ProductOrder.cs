namespace ServerBackendHooliTees.Models.Database.Entities;
public class ProductOrder
{
    public int Id { get; set; }
    public int Quantity { get; set; }

    public int ProductsId { get; set; }
    public string OrdersId { get; set; }

    //  Foreing keys
    public ICollection<Orders> Orders { get; set; }
    public ICollection<Products> Products { get; set; }

}
