using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;
using ServerBackendHooliTees.Models.Dtos;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace ServerBackendHooliTees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{

    private MyDbContext _dbContextHoolitees;
    private PasswordHasher<string> passwordHasher = new PasswordHasher<string>();
    //var passwordHasher = new PasswordHasher<string>();
    private readonly TokenValidationParameters _tokenParameters;

    public UserController(MyDbContext dbContext, IOptionsMonitor<JwtBearerOptions> jwtOptions)
    {
        //  Base de Datos
        _dbContextHoolitees = dbContext;

        //  JWToken
        _tokenParameters = jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme)
            .TokenValidationParameters;
    }


    [HttpGet("userlist")]
    public IEnumerable<UserSignDto> GetUser()
    {
        return _dbContextHoolitees.Users.Select(ToDto);
    }


    [HttpPost("signup")]
    public async Task<IActionResult> Post([FromForm] CreateUser userSignDto)
    {
        string hashedPassword = passwordHasher.HashPassword(userSignDto.Name, userSignDto.Password);

        Users newUser = new Users()
        {
            Name = userSignDto.Name,
            Email = userSignDto.Email,
            Password = hashedPassword,
            Address = userSignDto.Address
        };

        

        await _dbContextHoolitees.Users.AddAsync(newUser);
        await _dbContextHoolitees.SaveChangesAsync();

        UserSignDto userCreated = ToDto(newUser);


        ShoppingCart newCart = new ShoppingCart()
        {
            UserId = userCreated.Id,
            CartProductId = userCreated.Id
        };

        await _dbContextHoolitees.ShoppingCarts.AddAsync(newCart);
        await _dbContextHoolitees.SaveChangesAsync();

        return Created($"/{newUser.Id}", userCreated);
    }

    [HttpPost("login")]
    public IActionResult Login([FromForm]UserLoginDto userLoginDto)
    {
        //  Ejemplo
        //  var t = dbContextHoolitees.Users.FirstOrDefault(user => user.Email == "");

        foreach (Users userList in _dbContextHoolitees.Users.ToList())
        {
            if ( userList.Email == userLoginDto.Email )
            {
                //  Cifar los datos del usuario
                var result = passwordHasher.VerifyHashedPassword(userList.Name, userList.Password, userLoginDto.Password);

                if ( result == PasswordVerificationResult.Success)
                {
                    string rol = "";

                    if (userList.IsAdmin == true)
                    {
                        //string ro = "admin"
                        rol = "admin";
                    }

                    //  Creamos el Token
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        //  Datos para autorizar al usario
                        Claims = new Dictionary<string, object>
                    {
                        {"id", Guid.NewGuid().ToString() },
                        { ClaimTypes.Role, rol  }
                    },
                        //  Caducidad del Token
                        Expires = DateTime.UtcNow.AddDays(5),
                        //  Clave y algoritmo de firmado
                        SigningCredentials = new SigningCredentials(
                            _tokenParameters.IssuerSigningKey,
                            SecurityAlgorithms.HmacSha256Signature)
                    };

                    //  Creamos el token y lo devolvemos al usuario logeado
                    JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                    SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                    string stringToken = tokenHandler.WriteToken(token);


                    //return Ok(stringToken);
                    return Ok(new { StringToken = stringToken, userList.Id });

                }

            }
        }
        return Unauthorized("Usuario no existe");
    }
[HttpDelete("deleteUser/{userId}")]
public IActionResult DeleteUser(long userId)
{
    try
    {
        // Buscar el usuario en la base de datos
        var userToDelete = _dbContextHoolitees.Users.FirstOrDefault(user => user.Id == userId);

        if (userToDelete == null)
        {
            return NotFound($"Usuario con ID {userId} no encontrado");
        }

        // Eliminar el usuario
        _dbContextHoolitees.Users.Remove(userToDelete);
        _dbContextHoolitees.SaveChanges();

        return Ok(new { Message = "Usuario eliminado con éxito" });
    }
    catch (Exception ex)
    {
        return BadRequest(new { Error = $"Error al eliminar el usuario: {ex.Message}" });
    }
}
    [HttpPut("updateUserRole/{userId}")]
    public IActionResult UpdateUserRole(long userId, [FromBody] bool isAdmin)
    {
        try
        {
            // Buscar el usuario en la base de datos
            var userToUpdate = _dbContextHoolitees.Users.FirstOrDefault(user => user.Id == userId);

            if (userToUpdate == null)
            {
                return NotFound($"Usuario con ID {userId} no encontrado");
            }

            // Actualizar el rol
            userToUpdate.IsAdmin = isAdmin;
            _dbContextHoolitees.SaveChanges();

            return Ok(new { Message = "Rol de usuario actualizado con éxito" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = $"Error al actualizar el rol del usuario: {ex.Message}" });
        }
    }
    [HttpGet("userinfo/{userId}")]
    public IActionResult GetUserInfoById(long userId)
    {
        var user = _dbContextHoolitees.Users
            .Where(u => u.Id == userId)
            .Select(ToDto)
            .FirstOrDefault();

        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }
    private UserSignDto ToDto(Users users)
    {
        return new UserSignDto()
        {
            Id = (int)users.Id,
            Name = users.Name,
            Email = users.Email,
            Password = users.Password,
            Address = users.Address,
            isAdmin =users.IsAdmin
        };
    }

}
