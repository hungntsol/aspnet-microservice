using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Order.Domain.Entities.Common;

namespace Order.Infrastructure.Persistence;

public class OrderDataContext : DbContext
{
	public OrderDataContext(DbContextOptions options) : base(options)
	{
	}

	private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create((builder) =>
	{
		builder
			.AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Information)
			.AddFilter(DbLoggerCategory.Query.Name, LogLevel.Debug)
			.AddConsole();
	});

	public DbSet<Domain.Entities.Order> Orders { get; set; }

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);
		optionsBuilder.UseLoggerFactory(_loggerFactory);
		
		optionsBuilder.UseSqlServer(connection => 
			connection.MigrationsAssembly("Order.Infrastructure"));
	}

	public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
		CancellationToken cancellationToken = new CancellationToken())
	{
		foreach (var entry in ChangeTracker.Entries<EntityBase>())
		{
			switch (entry.State)
			{
				case EntityState.Added:
					entry.Entity.CreatedAt = DateTime.UtcNow;
					entry.Entity.CreatedBy = "swn";
					break;
				case EntityState.Modified:
					entry.Entity.LastModifiedAt = DateTime.UtcNow;
					entry.Entity.LastModifiedBy = "swn";
					break;
			}
		}

		return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
	}
}