using Domain.Entities;

namespace Domain.Interfaces;

public interface IAccountRepository
{
    User GetByEmail(string email);
    void Add(User user);
}
