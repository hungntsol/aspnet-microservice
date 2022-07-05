using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Persistence;
using Order.Domain.Entities.Common;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class AsyncRepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
{
    protected readonly OrderDataContext Context;

    public AsyncRepositoryBase(OrderDataContext context)
    {
        Context = context;
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await Context.Set<T>().Where(x => x.Id == id).FirstOrDefaultAsync();
    }
    

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await Context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>>? predicate, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, string? includeString, bool tracking = true)
    {
        IQueryable<T> dbSet = Context.Set<T>();
        
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
        IQueryable<T> dbSet = Context.Set<T>();
        
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
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        return await Context.SaveChangesAsync() > 0;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        Context.Remove(entity);
        return await Context.SaveChangesAsync() > 0;
    }
}