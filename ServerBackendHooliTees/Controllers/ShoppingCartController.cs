using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;

namespace ServerBackendHooliTees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShoppingCartController : ControllerBase
{

    private MyDbContext _dbContextHoolitees;

    public ShoppingCartController(MyDbContext dbContextHoolitees)
    {
        _dbContextHoolitees = dbContextHoolitees;
    }

    [HttpGet("shoppincart")]
    public IEnumerable<ShoppingCart> GetShoppingCartsView()
    {
        return _dbContextHoolitees.ShoppingCarts;
    }

    [HttpPost("addtoshopcart")]
    public async Task<IActionResult> addProduct([FromForm] Products product)
    {

        CartProduct addProduc = new CartProduct()
        {
            ProductsId = product.Id,
            ShoppingCartId = product.Id,
            Quantity = 1
        };

        await _dbContextHoolitees.CartProducts.AddAsync(addProduc);
        await _dbContextHoolitees.SaveChangesAsync();

        return Created($"/{product.Id}", addProduc); ;
    }

}
