using MassTransit;

namespace Basket.API.ExtensionMethods;

public static class EventBusExtensions
{
	public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMassTransit(conf =>
		{
			conf.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));
			});
		});

		return services;
	}
}