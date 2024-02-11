using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpPut("eliminarproductocarrito")]
    public async Task<IActionResult> DeleteProduct([FromForm] int productId, [FromForm] int userId)
    {
        var ListProduct = await _dbContextHoolitees.CartProducts
                            .FirstOrDefaultAsync(id => id.ShoppingCartId == userId && id.ProductId == productId);

        _dbContextHoolitees.CartProducts.Remove(ListProduct);

        await _dbContextHoolitees.SaveChangesAsync();

        return Ok("Producto eliminado");

    }

    [HttpPut("cambiarcantidad")]
    public async Task<IActionResult> ModifyProduct([FromForm] int productId, [FromForm] int userId, [FromForm] int quantity)
    {
        var product = await _dbContextHoolitees.CartProducts
                        .FirstOrDefaultAsync(id => id.ShoppingCartId == userId && id.ProductId == productId);

        if (product != null)
        {
            product.Quantity = quantity;
            _dbContextHoolitees.CartProducts.Update(product);
            await _dbContextHoolitees.SaveChangesAsync();
            return Ok("Producto actualizado");
        }
        else
        {
            return NotFound("Producto no encontrado en el carrito");
        }

    }

}
