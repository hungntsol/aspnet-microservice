using Catalog.API.Entities;

namespace Catalog.API.Repositories;

public interface IProductRepository
{
    // enumerable
    Task<IEnumerable<Product>> GetAllProductsAsync();
    Task<IEnumerable<Product>> GetProductsByCategoryAsync(string categoryName);

    // single item
    Task<Product> GetProductByIdAsync(string id);
    Task<Product> GetProductByNameAsync(string name);

    // insert
    Task InsertProductAsync(Product product);

    // update 
    Task<bool> UpdateProductAsync(Product product);

    // delete
    Task<bool> DeleteProductAsync(Product product);
}