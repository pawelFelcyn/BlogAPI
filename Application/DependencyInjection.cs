using Application.Authentication;
using Application.Authorization;
using Application.Services;
using Domain.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true));

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IUserContextService, UserContextService>();
        services.AddScoped<IPostService, PostService>();
        services.AddHttpContextAccessor();

        return services;
    }

    public static IServiceCollection AddAuthorizationHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, PostOperationRequirementHandler>();

        return services;
    }
}
