namespace ServerBackendHooliTees.Models.Dtos;

public class CreateProduct
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public IFormFile File { get; set; }
}
