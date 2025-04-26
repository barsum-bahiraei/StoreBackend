using Microsoft.EntityFrameworkCore;
using StoreBackend.Entities;

namespace StoreBackend.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
