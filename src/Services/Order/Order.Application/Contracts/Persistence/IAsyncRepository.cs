using System.Linq.Expressions;
using Order.Domain.Entities.Common;

namespace Order.Application.Contracts.Persistence;

public interface IAsyncRepository<TEntity> where TEntity : EntityBase
{
    // Get
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);

    // Find
    Task<IReadOnlyList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>>? predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        string? includeString,
        bool tracking = true);
    Task<IReadOnlyList<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>>? predicate,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy,
        IEnumerable<Expression<Func<TEntity, object>>>? includes,
        bool tracking = true);
    
    // Add
    Task<TEntity> AddAsync(TEntity entity);
    
    // Update
    Task<bool> UpdateAsync(TEntity entity);
    
    // Delete
    Task<bool> RemoveAsync(TEntity entity);
}