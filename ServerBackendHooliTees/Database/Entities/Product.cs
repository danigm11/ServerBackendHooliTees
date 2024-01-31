namespace ServerBackendHooliTees.Database.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock {  get; set; }
        public string Image {  get; set; }

        //  Claves Foraneas
        public ICollection<Users> UsersId { get; set; }
    }
}
