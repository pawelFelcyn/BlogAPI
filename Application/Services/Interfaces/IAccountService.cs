using Application.Dtos;

namespace Application.Services.Interfaces;

public interface IAccountService
{
    string GetToken(LoginDto dto);
    void Register(RegisterDto dto);
}
