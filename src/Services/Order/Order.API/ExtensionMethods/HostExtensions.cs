using Microsoft.EntityFrameworkCore;

namespace Order.API.ExtensionMethods;

public static class HostExtensions
{
	public static IHost MigrateDatabase<TContext>(this IHost host,
		Action<TContext, IServiceProvider> seeder,
		int? retry = 10) where TContext : DbContext
	{
		using var scope = host.Services.CreateScope();
		var services = scope.ServiceProvider;

		var logger = services.GetRequiredService<ILogger<TContext>>();
		var context = services.GetService<TContext>();

		try
		{
			logger.LogInformation("Start migrating database associated with context {DbContextName}",
				typeof(TContext).Name);

			InvokeMigrate(seeder, context, services);

			logger.LogInformation("Migrate database done");
		}
		catch (Exception)
		{
			retry--;
			Thread.Sleep(1000);
			MigrateDatabase<TContext>(host, seeder, retry);
		}

		return host;
	}

	private static void InvokeMigrate<TContext>(Action<TContext, IServiceProvider> seeder,
		TContext context,
		IServiceProvider services) where TContext : DbContext?
	{
		context?.Database.Migrate();
		seeder.Invoke(context, services);
	}
}