using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Persistence;
using Order.Domain.Entities.Common;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class AsyncRepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    private readonly OrderDataContext _context;

    public AsyncRepositoryBase(OrderDataContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _context.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }
    

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string? includeString, bool tracking = true)
    {
        IQueryable<T> dbSet = _context.Set<T>();
        
        if (tracking)
            dbSet.AsTracking();
        else
            dbSet.AsNoTracking();

        if (!string.IsNullOrEmpty(includeString))
            dbSet = dbSet.Include(includeString);

        if (predicate is not null)
        {
            dbSet = dbSet.Where(predicate);
        }

        if (orderBy is not null)
        {
            return await orderBy(dbSet).ToListAsync();
        }

        return await dbSet.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, IEnumerable<Expression<Func<T, object>>>? includes, bool tracking = true)
    {
        IQueryable<T> dbSet = _context.Set<T>();
        
        if (tracking)
            dbSet.AsTracking();
        else
            dbSet.AsNoTracking();

        foreach (var include in includes)
        {
            dbSet = dbSet.Include(include);
        }

        if (predicate is not null)
        {
            dbSet = dbSet.Where(predicate);
        }

        if (orderBy is not null)
        {
            return await orderBy(dbSet).ToListAsync();
        }

        return await dbSet.ToListAsync();
    }

    public async Task<T> AddAsync(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        _context.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}