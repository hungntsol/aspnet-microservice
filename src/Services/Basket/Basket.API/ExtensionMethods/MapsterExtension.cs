using Basket.API.Mapping;
using Mapster;
using MapsterMapper;

namespace Basket.API.ExtensionMethods;

public static class MapsterExtension
{
	public static IServiceCollection AddMapster(this IServiceCollection services)
	{
		var config = new TypeAdapterConfig();
		config.Apply(new MappingProfile());

		services.AddSingleton(config);
		services.AddTransient<IMapper, ServiceMapper>();
		
		return services;
	}
}