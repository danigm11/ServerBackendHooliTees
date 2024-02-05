﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServerBackendHooliTees.Models.Database;
using ServerBackendHooliTees.Models.Database.Entities;
using ServerBackendHooliTees.Models.Dtos;

namespace ServerBackendHooliTees.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private MyDbContext dbContextHoolitees;
        public ProductController(MyDbContext dbContext)
        {
            dbContextHoolitees = dbContext;
        }
        [HttpGet("productdetail")]
        public IEnumerable<ProductViewDto> GetProductView()
        {
            return dbContextHoolitees.Products.Select(ToDto);
        }
        /* [HttpGet("productcatalog")]
         public IEnumerable<ProductCartDto> GetProductCart()
         {
             return dbContextHoolitees.CartProducts.Select(CartDto);
         }
        */
        [HttpPost("productedit")]
        public async Task<IActionResult> Post([FromForm] ProductViewDto productView)
        {

            Products newProduct = new Products()
            {
                Name = productView.Name,
                Id = productView.Id,
                Image = productView.image,
                Description = productView.Description,
                Price = productView.Price,
                Stock = productView.Stock
            };

            await dbContextHoolitees.Products.AddAsync(newProduct);
            await dbContextHoolitees.SaveChangesAsync();

            ProductViewDto productCreated = ToDto(newProduct);

            return Created($"/{productView.Id}", productCreated);
        }
        private ProductViewDto ToDto(Products Products)
        {
            return new ProductViewDto
            {
                Id = Products.Id,
                Name = Products.Name,
                image = Products.Image,
                Description = Products.Description,
                Price = Products.Price,
                Stock = Products.Stock
            };
        }
       /* private ProductCartDto CartDto(CartProduct cartProduct)
        {
            return new ProductCartDto
            {
                Name = cartProduct.Name,
                Quantity = cartProduct.Quantity,
                Price = cartProduct.Products.Price,
                Image = cartProduct.Products.Image
            };
        }
       */
    }
}
