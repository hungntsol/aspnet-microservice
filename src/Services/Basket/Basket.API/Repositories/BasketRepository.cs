using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Basket.API.Repositories;

public class BasketRepository : IBasketRepository
{
    private readonly IDistributedCache _distributedCache;

    public BasketRepository(IDistributedCache distributedCache)
    {
        _distributedCache = distributedCache;
    }

    public async Task<ShoppingCart?> GetBasketAsync(string userName)
    {
        ArgumentNullException.ThrowIfNull(userName);
        
        var basket = await _distributedCache.GetStringAsync(userName);
        if (string.IsNullOrEmpty(basket))
            return null;

        return JsonConvert.DeserializeObject<ShoppingCart>(basket) ?? null;
    }

    public async Task<ShoppingCart?> UpdateBasketAsync(ShoppingCart basket)
    {
        ArgumentNullException.ThrowIfNull(basket.Username);

        await _distributedCache.SetStringAsync(basket.Username, JsonConvert.SerializeObject(basket));

        return await GetBasketAsync(basket.Username) ?? null;
    }

    public async Task<bool> DeleteBasketAsync(string userName)
    {
        try
        {
            await _distributedCache.RemoveAsync(userName);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}