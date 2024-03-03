using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;
using ServerBackendHooliTees.Models.Dtos;

namespace ServerBackendHooliTees.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private MyDbContext _dbContextHoolitees;
    
    private const string PRODUCTS_PATH = "images";

    public ProductController(MyDbContext dbContext)
    {
        _dbContextHoolitees = dbContext;
    }

    [HttpGet("productdetail")]
    public IEnumerable<ProductDto> GetProductView()
    {
        return _dbContextHoolitees.Products.Select(ToDto);
    }


    [HttpPost("createProduct")]
    public async Task<IActionResult> Post([FromForm] CreateProduct productDto)
    {
        if (productDto.File == null)
        {
            // Manejar el caso en el que el archivo no está presente
            return BadRequest("No se ha proporcionado un archivo.");
        }

        using Stream stream = productDto.File.OpenReadStream();
        string productPath = $"{Guid.NewGuid()}_{productDto.File.FileName}";
        string productImage = await FileService.SaveAsync(stream, PRODUCTS_PATH, productPath);

        Products newProduct = new Products()
        {
            Name = productDto.Name,
            Description = productDto.Description,
            Price = productDto.Price,
            Stock = productDto.Stock,
            Image = productImage

        };

        await _dbContextHoolitees.Products.AddAsync(newProduct);
        await _dbContextHoolitees.SaveChangesAsync();

        ProductDto productCreated = ToDto(newProduct);

        return Created($"/{newProduct.Id}", productCreated);
    }
    [HttpPut("modifyProduct/{productId}")]
public async Task<IActionResult> ModifyProduct(int productId, [FromForm] ModifyProduct modifiedProductDto)
{
    try
    {
        // Verificar si el productId es válido
        if (productId <= 0)
        {
            return BadRequest("ID de producto no válido");
        }

        // Buscar el producto en la base de datos
        var productToUpdate = await _dbContextHoolitees.Products.FindAsync(productId);

        if (productToUpdate == null)
        {
            return NotFound($"Producto con ID {productId} no encontrado");
        }

        // Actualizar los campos del producto con los nuevos valores
        productToUpdate.Name = modifiedProductDto.Name ?? productToUpdate.Name;
        productToUpdate.Description = modifiedProductDto.Description ?? productToUpdate.Description;
        productToUpdate.Price = modifiedProductDto.Price != 0 ? modifiedProductDto.Price : productToUpdate.Price;
        productToUpdate.Stock = modifiedProductDto.Stock >= 0 ? modifiedProductDto.Stock : productToUpdate.Stock;

        // Si hay un nuevo archivo, actualizar la imagen
        if (modifiedProductDto.File != null)
        {
            using Stream stream = modifiedProductDto.File.OpenReadStream();
            string productPath = $"{Guid.NewGuid()}_{modifiedProductDto.File.FileName}";
            productToUpdate.Image = await FileService.SaveAsync(stream, PRODUCTS_PATH, productPath);
        }

        // Guardar los cambios en la base de datos
        await _dbContextHoolitees.SaveChangesAsync();

        return Ok(new { Message = "Producto modificado con éxito" });
    }
    catch
    {
        // Manejo de errores
        return BadRequest(new { Error = "Error al modificar el producto" });
    }
}




    private ProductDto ToDto(Products products)
    {
        return new ProductDto()
        {
            Id  = products.Id,
            Name = products.Name,
            Description = products.Description,
            Price = products.Price,
            Stock = products.Stock,
            image = products.Image
        };
    }

}

