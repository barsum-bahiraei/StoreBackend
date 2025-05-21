﻿namespace StoreBackend.Models;

public class ProductCreateDto
{
    public string Name { get; set; }
    public int? Description { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string? ImageUrl { get; set; }
}
