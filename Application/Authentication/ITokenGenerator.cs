using Domain.Entities;

namespace Application.Authentication;

public interface ITokenGenerator
{
    string GetTokenStr(User user);
}
