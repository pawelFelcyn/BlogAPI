using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly BlogDbContext _dbContext;

    public AccountRepository(BlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public User GetByEmail(string email)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            throw new InvalidEmailException();
        }

        return user;
    }

    public void Add(User user)
    {
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }
}
