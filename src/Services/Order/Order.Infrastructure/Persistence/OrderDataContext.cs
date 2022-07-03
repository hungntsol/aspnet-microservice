using Microsoft.EntityFrameworkCore;
using Order.Domain.Entities.Common;

namespace Order.Infrastructure.Persistence;

public class OrderDataContext : DbContext
{
    public OrderDataContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Domain.Entities.Order> Orders { get; set; }

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
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