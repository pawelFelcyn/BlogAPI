using Domain.Entities;
using Infrastructure.Data;

namespace Infrastructure.Tests.Extensions;

internal static class BlogDbContextExtensions
{
    public static User SeedUser(this BlogDbContext dbContext)
    {
        var user  = new User()
        {
            Email = "email",
            FirstName = "",
            LastName = "",
            Nick = "",
            Role = "",
            PasswordHash = ""
        };

        dbContext.Users.Add(user);
        dbContext.SaveChanges();

        return user;
    }
}
