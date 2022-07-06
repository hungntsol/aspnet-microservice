using System.Reflection;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Order.API.Mapping;
using Order.Application.Behaviours;
using Order.Application.Features.Orders.Queries.GetOneOrder;
using Order.Application.Mapping;

namespace Order.API.ExtensionMethods;

public static class ApplicationServicesExtension
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{
		AddMapster(services);
		
		services.AddMediatR(typeof(GetOneOrderByIdQuery).Assembly);
		services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

		AddPipelines(services);

		return services;
	}

	private static void AddPipelines(IServiceCollection services)
	{
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledBehaviour<,>));
	}

	private static void AddMapster(IServiceCollection services)
	{
		var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
		typeAdapterConfig.Apply(new MappingProfile());
		typeAdapterConfig.Apply(new MappingApiProfile());

		services.AddSingleton(typeAdapterConfig);
		services.AddScoped<IMapper, ServiceMapper>();
	}
}