namespace ServerBackendHooliTees.Models.Dtos;

public class UserSignDto
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Email { get; set; }
    
    public string Password { get; set; }

    public string Address { get; set; }

   public bool isAdmin { get; set; }

}
