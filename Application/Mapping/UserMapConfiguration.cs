using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping;

internal class UserMapConfiguration : IMapConfiguration
{
    public void ConfigureMappings(Profile profile)
    {
        profile.CreateMap<RegisterDto, User>();
    }
}
