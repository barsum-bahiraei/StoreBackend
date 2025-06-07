using Microsoft.EntityFrameworkCore;
using StoreBackend.Data;
using StoreBackend.Entities;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;

namespace StoreBackend.Services.Implementation;

public class ProductService(DatabaseContext context) : IProductService
{

    public async Task<ProductDetailViewModel?> Detail(int id)
    {
        var result = await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null) return null;
        return new ProductDetailViewModel
        {
            Id = result.Id,
            Name = result.Name,
            Description = result.Description,
            Price = result.Price,
            Discount = result.Discount,
            ImageUrl = result.ImageUrl,
        };
    }

    public async Task<int> Create(ProductCreateParameters productCreateDTO)
    {
        var product = new Product
        {
            Name = productCreateDTO.Name,
            Description = productCreateDTO.Description,
            Discount = productCreateDTO.Discount,
            Price = productCreateDTO.Price,
            ImageUrl = productCreateDTO.ImageUrl,
        };
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();

        return product.Id;
    }
}
