using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreBackend.Entities;

namespace StoreBackend.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
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
    }
}
