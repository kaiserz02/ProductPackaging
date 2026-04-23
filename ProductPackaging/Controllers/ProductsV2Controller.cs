using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductPackaging.Data;
using ProductPackaging.DTOs;
using ProductPackaging.Entities;

namespace ProductPackaging.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/products")]
    [Authorize]
    public class ProductsV2Controller : ControllerBase
    {
        private readonly AppDbContext _db;

        public ProductsV2Controller(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductV2ResponseDto>>> GetProducts()
        {
            var products = await _db.Products
                .Include(p => p.Packages)
                    .ThenInclude(p => p.PackageType)
                .Include(p => p.Packages)
                    .ThenInclude(p => p.PackagingItems)
                        .ThenInclude(pi => pi.Item)
                .ToListAsync();

            var result = products.Select(p => new ProductV2ResponseDto
            {
                ProductID = p.ProductId,
                ProductName = p.ProductName,
                Packages = BuildPackages(p.Packages.ToList(), null)
            }).ToList();
            return Ok(result);
        }

        private List<PackageDto> BuildPackages(List<Packaging> allPackages, int? parentId)
        {
            return allPackages
                .Where(p => p.ParentPackageId == parentId)
                .Select(p => new PackageDto
                {
                    PackageID = p.PackageId,
                    PackageTypeID = p.PackageTypeId,
                    PackageTypeName = p.PackageType.PackageTypeName,
                    ParentID = p.ParentPackageId,

                    Items = p.PackagingItems.Select(i => new ItemDto
                    {
                        ItemID = i.Item.ItemId,
                        ItemName = i.Item.ItemName
                    }).ToList(),

                    Packages = BuildPackages(allPackages, p.PackageId)
                })
                .ToList();
        }
    }
}