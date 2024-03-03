namespace ServerBackendHooliTees.Models.Database.Entities;

public class Transaction
{

    public int Id { get; set; }
    public string ClientWallet { get; set; }
    public string Value { get; set; }
    public string Hash { get; set; }
    public bool Completed { get; set; }
    public int userId { get; set; }

    public decimal price { get; set; }  
    public string fecha { get; set; }

}
