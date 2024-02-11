using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    public async Task<IActionResult> AddProduct([FromForm]int productId, [FromForm] int userId, [FromForm] int quantity)
    {

        var product = await _dbContextHoolitees.CartProducts
                        .FirstOrDefaultAsync(id => id.ShoppingCartId == userId && id.ProductId == productId);

        if (product == null) 
        {

            CartProducts addProduct = new CartProducts()
            {
                ProductId = productId,
                ShoppingCartId = userId,
                Quantity = quantity
            };

            await _dbContextHoolitees.CartProducts.AddAsync(addProduct);
            await _dbContextHoolitees.SaveChangesAsync();

            return Created($"/{productId}", addProduct);

        } else
        {
            product.Quantity += quantity;
            _dbContextHoolitees.CartProducts.Update(product);
            await _dbContextHoolitees.SaveChangesAsync();
            return Ok("Producto actualizado");
        }

    }

}
