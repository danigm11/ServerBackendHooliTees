namespace ServerBackendHooliTees.Models.Database.Entities;

public class TransactionToSing
{

    public int Id { get; set; }
    public string From { get; init; }
    public string To { get; init; }
    public string Value { get; init; }
    public string Gas { get; init; }
    public string GasPrice { get; init; }

}
