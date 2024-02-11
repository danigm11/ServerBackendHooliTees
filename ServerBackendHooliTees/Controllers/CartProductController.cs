using Microsoft.AspNetCore.Mvc;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;


namespace ServerBackendHooliTees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartProductController : ControllerBase
{
    private MyDbContext _dbContextHoolitees;

    public CartProductController(MyDbContext dbContextHoolitees)
    {
        _dbContextHoolitees = dbContextHoolitees;
    }
    [HttpGet("productosCarrito")]
    public IEnumerable<CartProducts> GetProductosCarrito()
    {
        return _dbContextHoolitees.CartProducts;
    }
}
