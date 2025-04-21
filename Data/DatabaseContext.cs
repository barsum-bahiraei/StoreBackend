using Microsoft.EntityFrameworkCore;
using StoreBackend.Models;

namespace StoreBackend.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}
