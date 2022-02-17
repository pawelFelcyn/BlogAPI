using Domain.Interfaces;
using Infrastructure.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddHelpers(this IServiceCollection services)
    {
        services.AddScoped<IEmailValidationHelper, EmailValidationHelper>();

        return services;
    }
}
