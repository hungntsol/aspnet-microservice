using Order.Application.Contracts.Persistence;
using Order.Application.Contracts.Services;
using Order.Infrastructure.Repositories;
using Order.Infrastructure.Services;

namespace Order.API.ExtensionMethods;

public static class InfrastructureServicesExtension
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
	{
		services.AddTransient(typeof(IAsyncRepository<>), typeof(AsyncRepositoryBase<>));
		services.AddTransient<IOrderRepository, OrderRepository>();
		services.AddTransient<IEmailService, MailService>();

		return services;
	}
}