using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.ExtensionMethods;

public static class HttpClientExtension
{
	public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddHttpClient<ICatalogService, CatalogService>(c =>
			c.BaseAddress = new Uri(configuration.GetValue<string>("ApiSettings:CatalogUrl")));
		services.AddHttpClient<IBasketService, BasketService>(c => 
			c.BaseAddress = new Uri(configuration.GetValue<string>("ApiSettings:BasketUrl")));
		services.AddHttpClient<IOrderService, OrderService>(c =>
			c.BaseAddress = new Uri(configuration.GetValue<string>("ApiSettings:OrderUrl")));
		
		return services;
	}
}