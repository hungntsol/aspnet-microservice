using Microsoft.EntityFrameworkCore;

namespace Order.API.ExtensionMethods;

public static class DataContextExtensions
{
	public static IServiceCollection AddDataContext<TContext>(this IServiceCollection services, IConfiguration configuration)
		where TContext : DbContext
	{
		var connectionString = configuration.GetValue<string>("MSSQLSettings:ConnectionString");
		services.AddDbContext<TContext>(options =>
		{
			options.UseSqlServer(connectionString);
		});

		return services;
	}
}