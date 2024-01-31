namespace ServerBackendHooliTees.Database.Entities
{
    public class Producto_Orden
    {
        public ICollection<Orders> Orders { get; set; }
        public ICollection<Product> Products { get; set; }
        public int Quantity { get; set; }
    }
}
