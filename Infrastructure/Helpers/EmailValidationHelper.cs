using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Helpers;

public class EmailValidationHelper : IEmailValidationHelper
{
    private readonly BlogDbContext _dbContext;

    public EmailValidationHelper(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public bool IsTaken(string email)
    {
        return _dbContext.Users.Any(u => u.Email == email);
    }
}
