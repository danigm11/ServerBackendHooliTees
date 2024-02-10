namespace ServerBackendHooliTees.Models.Dtos
{
    public class CreateImageDto
    {
        public string Name { get; set; }
        public IFormFile File { get; set; }
    }
}
