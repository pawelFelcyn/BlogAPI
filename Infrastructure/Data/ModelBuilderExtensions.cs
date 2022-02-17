using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public static class ModelBuilderExtensions
{
    public static ModelBuilder OnUserCreating(this ModelBuilder builder)
    {
        builder.Entity<User>()
            .Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Entity<User>()
            .Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(20);

        builder.Entity<User>()
            .Property(u => u.Nick)
            .IsRequired()
            .HasMaxLength(30);

        builder.Entity<User>()
            .Property(u => u.DateOfAppending)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.Role)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.Email)
            .IsRequired();

        builder.Entity<User>()
            .Property(u => u.PasswordHash)
            .IsRequired();

        return builder;
    }
}
