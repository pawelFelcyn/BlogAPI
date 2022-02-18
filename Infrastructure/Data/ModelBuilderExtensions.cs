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

    public static ModelBuilder OnPostCraeting(this ModelBuilder builder)
    {
        builder.Entity<Post>()
            .Property(p => p.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Entity<Post>()
            .Property(p => p.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Entity<Post>()
            .Property(p => p.Created)
            .IsRequired();

        builder.Entity<Post>()
            .Property(p => p.LastModyfied)
            .IsRequired();

        builder.Entity<Post>()
            .Property(p => p.CreatedById)
            .IsRequired();

        return builder;
    }
}
