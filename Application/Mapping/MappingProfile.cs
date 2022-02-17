using AutoMapper;
using System.Reflection;

namespace Application.Mapping;

internal class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingsFromAssembly(Assembly assembly)
    {
        var types = assembly.GetTypes()
                    .Where(t => typeof(IMapConfiguration).IsAssignableFrom(t) && !t.IsInterface)
                    .ToList();

        foreach (var type in types)
        {
            var instance = Activator.CreateInstance(type);
            var methodInfo = type.GetMethod(nameof(IMapConfiguration.ConfigureMappings));
            methodInfo?.Invoke(instance, new[] { this });
        }
    }
}
