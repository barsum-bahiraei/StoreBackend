using Microsoft.EntityFrameworkCore;
using StoreBackend.Data;
using StoreBackend.Entities;
using StoreBackend.Models;
using StoreBackend.Services.Contracts;

namespace StoreBackend.Services.Implementation;

public class ProductService(DatabaseContext context) : IProductService
{

    public async Task<ProductDetailViewModel?> Detail(int id, CancellationToken cancellation)
    {
        var result = await context.Products.FirstOrDefaultAsync(x => x.Id == id, cancellation);
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

    public async Task<int> Create(ProductCreateParameters parameters, CancellationToken cancellation)
    {
        var product = new Product
        {
            Name = parameters.Name,
            Description = parameters.Description,
            Discount = parameters.Discount,
            Price = parameters.Price,
            ImageUrl = parameters.ImageUrl,
        };
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync(cancellation);

        return product.Id;
    }

    public async Task<List<ProductListViewModel>> List(ProductListParameters parameters, CancellationToken cancellation)
    {
        var products = await context.Products
            .Where(x => x.Name == parameters.Name)
            .OrderBy(x => x.Id)
            .Select(x => new ProductListViewModel
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync(cancellation);
        return products;
    }
}
