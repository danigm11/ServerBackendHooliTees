namespace ServerBackendHooliTees.Database.Entities
{
    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public string Image {  get; set; }

        //  Foreign Keys
        public ICollection<CartProduct> CartProduct { get; set; }
        public ICollection<ProductOrder> ProductOrder { get; set; }

    }
}
