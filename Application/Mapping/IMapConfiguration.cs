using AutoMapper;

namespace Application.Mapping;

internal interface IMapConfiguration
{
    void ConfigureMappings(Profile profile);
}
