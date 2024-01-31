namespace ServerBackendHooliTees.Database.Entities
{
    public class Orders
    {
        public int OrdersId { get; set; }
        public decimal PriceETH {  get; set; }
        public string Hash {  get; set; }
        public string ClientWallet {  get; set; }
        public decimal TotalPrice {  get; set; }
        public string Status {  get; set; }
        public DateOnly Date {  get; set; }
        public ICollection<Users> Users { get; set; }
    }
}
