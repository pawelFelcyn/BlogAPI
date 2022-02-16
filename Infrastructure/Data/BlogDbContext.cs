using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class BlogDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public BlogDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.OnUserCreating();
    }
}
