using Basket.API.ExtensionMethods;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = configuration.GetValue<string>("RedisSettings:ConnectionString");
});

builder.Services.AddTransient<IBasketRepository, BasketRepository>();
builder.Services.AddGrpcClient<DiscountService.DiscountServiceClient>(
    o => o.Address = new Uri(configuration.GetValue<string>("GrpcSettings:DiscountServiceUrl")));
builder.Services.AddTransient<DiscountGrpcService>();

builder.Services.AddMapster();
builder.Services.AddEventBus(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();