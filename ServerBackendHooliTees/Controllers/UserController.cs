using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;
using ServerBackendHooliTees.Models.Dtos;
using System.IO;
using System.Security.Claims;

namespace ServerBackendHooliTees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private MyDbContext dbContextHoolitees;
    private PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
    //var passwordHasher = new PasswordHasher<string>();

    public UserController(MyDbContext dbContext)
    {
        dbContextHoolitees = dbContext;
    }


    [HttpGet("userlist")]
    public IEnumerable<UserSignDto> GetUser()
    {
        return dbContextHoolitees.Users.Select(ToDto);
    }


    [HttpPost("signup")]
    public async Task<IActionResult> Post([FromForm] UserSignDto userSignDto)
    {
        string hashedPassword = passwordHasher.HashPassword(userSignDto.Name, userSignDto.Password);

        Users newUser = new Users()
        {
            Name = userSignDto.Name,
            Email = userSignDto.Email,
            Password = hashedPassword,
            Address = userSignDto.Address
        };

        await dbContextHoolitees.Users.AddAsync(newUser);
        await dbContextHoolitees.SaveChangesAsync();

        UserSignDto userCreated = ToDto(newUser);

        return Created($"/{newUser.Id}", userCreated);
    }

    [HttpPost("login")]
    public async Task<Boolean> Post([FromForm]UserLoginDto userLoginDto)
    {
        //  Ejemplo
        //  var t = dbContextHoolitees.Users.FirstOrDefault(user => user.Email == "");

        foreach (Users userList in dbContextHoolitees.Users.ToList())
        {
            if ( userList.Email == userLoginDto.Email )
            {
                var result = passwordHasher.VerifyHashedPassword(userList.Name, userList.Password, userLoginDto.Password);

                if (result == PasswordVerificationResult.Success)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private UserSignDto ToDto(Users users)
    {
        return new UserSignDto()
        {
            Name = users.Name,
            Email = users.Email,
            Password = users.Password,
            Address = users.Address
        };
    }

}
