using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductPackaging.Data;
using ProductPackaging.DTOs;
using ProductPackaging.Entities;

namespace ProductPackaging.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [Authorize]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductV1ResponseDto>>> GetProducts()
        {
            var products = await _db.Products
                .Select(p => new ProductV1ResponseDto
                {
                    ProductID = p.ProductId,
                    ProductName = p.ProductName
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName
            };

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}