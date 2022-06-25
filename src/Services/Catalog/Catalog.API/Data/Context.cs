using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class Context : IContext
{
    private const string DATABASE_SETTINGS_PREFIX = "DatabaseSettings";
    
    public Context(IConfiguration configuration)
    {
        var connectionString = configuration.GetValue<string>($"{DATABASE_SETTINGS_PREFIX}:ConnectionString");
        var databaseName = configuration.GetValue<string>($"{DATABASE_SETTINGS_PREFIX}:DatabaseName");
        
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        ProductsCollection = database.GetCollection<Product>(configuration.GetValue<string>($"{DATABASE_SETTINGS_PREFIX}:CollectionName"));
        
        // seed catalog
        SeedDataCatalog.Seed(ProductsCollection);
    }

    public IMongoCollection<Product> ProductsCollection { get; }
}