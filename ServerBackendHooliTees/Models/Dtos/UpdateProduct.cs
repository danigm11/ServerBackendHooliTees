using Microsoft.AspNetCore.Http;

namespace ServerBackendHooliTees.Models.Dtos
{
    public class ModifyProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile File { get; set; }
    }
}

