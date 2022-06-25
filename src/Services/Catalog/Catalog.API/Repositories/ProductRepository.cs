using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IContext _context;

    public ProductRepository(IContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        return await _context.ProductsCollection.FindAsync(FilterDefinition<Product>.Empty)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult()
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName)
    {
        return await _context.ProductsCollection.FindAsync(x => x.Category == categoryName)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult()
            .ToListAsync();
    }

    public async Task<Product> GetProductByIdAsync(string id)
    {
        return await _context.ProductsCollection.FindAsync(x => x.Id == id)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult()
            .FirstOrDefaultAsync();
    }

    public async Task<Product> GetProductByNameAsync(string name)
    {
        return await _context.ProductsCollection.FindAsync(x => x.Name == name)
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult()
            .FirstOrDefaultAsync();
    }

    public async Task InsertProductAsync(Product product)
    {
        await _context.ProductsCollection.InsertOneAsync(product);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var updated = await _context.ProductsCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product)
            .ConfigureAwait(false);

        return updated is not null;
    }

    public async Task<bool> DeleteProductAsync(Product product)
    {
        var deleted = await _context.ProductsCollection.DeleteOneAsync(x => x.Id == product.Id)
            .ConfigureAwait(false);
        return deleted.IsAcknowledged && deleted.DeletedCount > 0;
    }
}