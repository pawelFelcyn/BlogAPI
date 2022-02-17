using Application.Authentication;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

internal class AccountService : IAccountService
{
    private readonly IAccountRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ITokenGenerator _tokenGenerator;

    public AccountService(IAccountRepository repository, IMapper mapper, IPasswordHasher<User> passwordHasher,
        ITokenGenerator tokenGenerator)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public string GetToken(LoginDto dto)
    {
        var user = _repository.GetByEmail(dto.Email);

        var verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

        if (verificationResult != PasswordVerificationResult.Success)
        {
            throw new InvalidPasswordException();
        }

        return _tokenGenerator.GetTokenStr(user);
    }

    public void Register(RegisterDto dto)
    {
        var user = _mapper.Map<User>(dto);

        var hashedPassword = _passwordHasher.HashPassword(user, dto.Password);
        user.PasswordHash = hashedPassword;

        _repository.Add(user);
    }
}
