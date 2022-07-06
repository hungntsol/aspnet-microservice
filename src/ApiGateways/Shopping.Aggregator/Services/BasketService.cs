using System.Net;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class BasketService : IBasketService
{
	private readonly HttpClient _client;

	public BasketService(HttpClient client)
	{
		_client = client;
	}

	public async Task<BasketModel?> GetBasketAsync(string username)
	{
		var httpResponse = await _client.GetAsync($"api/Baskets/{username}");

		if (httpResponse.StatusCode == HttpStatusCode.OK)
		{
			return await httpResponse.Content.ReadFromJsonAsync<BasketModel>();
		}

		return null;
	}
}