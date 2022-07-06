using EventBus.Message.Common;
using MassTransit;
using Order.API.EventBusConsumers;

namespace Order.API.ExtensionMethods;

public static class EventBusExtension
{
	public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMassTransit(config =>
		{
			config.AddConsumer<BasketCheckoutConsumer>();
			config.UsingRabbitMq((ctx, cfg) =>
			{
				cfg.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));
				cfg.ReceiveEndpoint(EventBusMessage.BASKET_CHECKOUT_QUEUE, endpoint =>
				{
					endpoint.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
				});
			});
		});

		services.AddScoped<BasketCheckoutConsumer>();
		
		return services;
	}
}