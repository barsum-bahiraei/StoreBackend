﻿using Microsoft.EntityFrameworkCore;
using StoreBackend.Data.Configurations;
using StoreBackend.Entities;

namespace StoreBackend.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext()
    {

    }
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new ProductConfiguration());


        // modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}
