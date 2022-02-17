using Application.Dtos;

namespace Application.Services;

public interface IAccountService
{
    string GetToken(LoginDto dto);
    void Register(RegisterDto dto);
}
