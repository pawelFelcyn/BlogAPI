using Domain.Entities;

namespace Application.Authentication;

public interface ITokenGeterator
{
    string GetTokenStr(User user);
}
