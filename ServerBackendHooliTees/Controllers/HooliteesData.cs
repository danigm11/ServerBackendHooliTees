using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerBackendHooliTees.Database;
using ServerBackendHooliTees.Database.Entities;

namespace ServerBackendHooliTees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HooliteesData : ControllerBase
{

    private MyDbContext dbContextHoolitees;

    public HooliteesData(MyDbContext dbContext)
    {
        dbContextHoolitees = dbContext;
    }

    [HttpGet]
    public IEnumerable<Users> GetUsers()
    {
        return dbContextHoolitees.Users;
    }

}
