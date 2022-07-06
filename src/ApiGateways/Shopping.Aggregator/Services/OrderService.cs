using System.Net;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class OrderService : IOrderService
{
	private readonly HttpClient _client;

	public OrderService(HttpClient client)
	{
		_client = client;
	}

	public async Task<IEnumerable<OrderResponseModel>?> GetEnumerableOrdersAsync(string username)
	{
		var httpResponse = await _client.GetAsync($"/api/Orders/{username}");
		if (httpResponse.StatusCode == HttpStatusCode.OK)
		{
			return await httpResponse.Content.ReadFromJsonAsync<IEnumerable<OrderResponseModel>>();
		}

		return null;
	}
}