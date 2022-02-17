using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

internal class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AccountService(IAccountRepository repository, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public string GetToken(LoginDto dto)
    {
        throw new NotImplementedException();
    }

    public void Register(RegisterDto dto)
    {
        var user = _mapper.Map<User>(dto);

        var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
        user.PasswordHash = hashedPassword;

        _repository.Add(user);
    }
}
