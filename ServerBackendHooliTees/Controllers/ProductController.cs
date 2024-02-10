using EjemploImagesBackend;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;
using ServerBackendHooliTees.Models.Dtos;
using static System.Net.Mime.MediaTypeNames;

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

