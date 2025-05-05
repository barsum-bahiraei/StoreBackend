using Microsoft.EntityFrameworkCore;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(e => e.Family)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(e => e.Email)
                  .IsRequired()
                  .HasMaxLength(255);

            entity.HasIndex(e => e.Email)
                  .IsUnique();

            entity.Property(e => e.Password)
                  .IsRequired()
                  .HasMaxLength(20);
        });
    }
}
