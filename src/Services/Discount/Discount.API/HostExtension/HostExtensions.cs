using System;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;

namespace Discount.API.HostExtension
{
    public static class HostExtensions
    {
        public static IHost MigratePostgresDatabase<TContext>(this IHost host, int retry = 10)
        {
            using var scope = host.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            var configurationService = serviceProvider.GetRequiredService<IConfiguration>();
            var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();

            try
            {
                logger.LogInformation("Migrate discount db PostgresSQL");
                using var connection =
                    new NpgsqlConnection(configurationService.GetValue<string>("PostgresSettings:ConnectionString"));
                connection.Open();

                const string query = @"SELECT 1 FROM pg_database WHERE datname = 'coupon'";
                var result = connection.QueryFirstOrDefault<int>(query);
                logger.LogInformation("PostgresSQL database coupon exists: {0}", result);
                if (result == 1)
                {
                    logger.LogInformation("Coupon db PostgresSQL already exists");
                    return host; //end
                }

                using var command = new NpgsqlCommand() { Connection = connection };
                command.CommandText =
                    "CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, ProductName VARCHAR(100), Description TEXT,Amount INT);";
                command.ExecuteNonQuery();
                logger.LogInformation("Coupon db PostgresSQL created");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred while migrating the database");
                if (retry > 0)
                {
                    retry--;
                    System.Threading.Thread.Sleep(2000);
                    MigratePostgresDatabase<TContext>(host, retry);
                }
            }

            return host;
        }
    }
}