using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    private NpgsqlConnection _connection;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration;

        var connectionString = _configuration.GetValue<string>("PostgresSettings:ConnectionString");
        _connection = new NpgsqlConnection(connectionString);
        _connection.Open();
    }

    public async Task<Coupon> GetDiscountAsync(string productName)
    {
        const string query = "SELECT * FROM Coupon WHERE ProductName = @ProductName";
        var queryParams = new
        {
            ProductName = productName
        };

        var data = await _connection.QueryFirstOrDefaultAsync<Coupon>(query, queryParams);
        return data;
    }

    public async Task<bool> SaveDiscountAsync(Coupon coupon)
    {
        const string command = "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)";
        var commandParams = new
        {
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };
            
        var result = await _connection.ExecuteAsync(command, commandParams);
        return result > 0;
    }

    public async Task<bool> UpdateDiscountAsync(Coupon coupon)
    {
        const string command = "UPDATE Coupon SET ProductName=@ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id";
        var queryParams = new
        {
            Id = coupon.Id,
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount
        };
            
        var result = await _connection.ExecuteAsync(command, queryParams);
        return result > 0;
    }

    public async Task<bool> DeleteDiscountAsync(string productName)
    {
        const string command = "DELETE FROM Coupon WHERE ProductName = @ProductName";
        var queryParams = new
        {
            ProductName = productName
        };
            
        var result = await _connection.ExecuteAsync(command, queryParams);
        return result > 0;
    }
}