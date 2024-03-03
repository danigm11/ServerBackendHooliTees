namespace ServerBackendHooliTees.Models.Dtos;

public class ModifyUserRoleDto
{
    public long UserId { get; set; }
    public bool IsAdmin { get; set; }
}
