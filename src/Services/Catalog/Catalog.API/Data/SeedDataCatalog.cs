using Catalog.API.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.Data;

public class SeedDataCatalog
{
    public static void Seed(IMongoCollection<Product> collection)
    {
        var existedDoc = collection.Find(x => true).Any();
        if (existedDoc) 
            return;

        collection.InsertMany(FakeDocument());
    }

    private static IEnumerable<Product> FakeDocument()
    {
        return new List<Product>()
        {
            new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Iphone 12",
                Summary = "This is the newest flagship iphone 12",
                Description = "From California, made in China",
                Price = 500.45M,
                Category = "phone"
            },
            new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Nokia 8.1",
                Summary = "This is the newest nokia phone",
                Description = "From HMD Global",
                Price = 400M,
                Category = "phone"
            },
            new()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Realmi 6 pro",
                Summary = "This is the newest realmi phone",
                Description = "From Realmi",
                Price = 300M,
                Category = "phone"
            }
        };
    }
}