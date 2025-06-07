﻿using Microsoft.EntityFrameworkCore;
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
        IQueryable<Product> query = context.Products.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(parameters.Name))
        {
            query = query.Where(x => EF.Functions.Like(x.Name, $"%{parameters.Name.Trim()}%"));
        }
        var products = await query
            .OrderBy(x => x.Id)
            .Select(x => new ProductListViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Price = x.Price,
                Discount = x.Discount,
                ImageUrl = x.ImageUrl,
            })
            .ToListAsync(cancellation);
        return products;
    }
}
