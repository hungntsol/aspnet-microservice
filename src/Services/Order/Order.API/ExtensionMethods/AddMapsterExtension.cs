using Mapster;
using Order.Application.Mapping;

namespace Order.API.ExtensionMethods;

public static class AddMapsterExtension
{
    public static void AddMapster(this IServiceCollection services)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(typeof(MappingProfile).Assembly);
    }
}