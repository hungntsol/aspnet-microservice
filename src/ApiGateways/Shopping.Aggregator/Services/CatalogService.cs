using System.Net;
using Shopping.Aggregator.Models;

namespace Shopping.Aggregator.Services;

public class CatalogService : ICatalogService
{
	private readonly HttpClient _client;

	public CatalogService(HttpClient client)
	{
		_client = client;
	}

	public async Task<IEnumerable<CatalogModel>> GetEnumerableCatalogsAsync()
	{
		var httpResponse = await _client.GetAsync("/api/Catalogs");

		return (await httpResponse.Content.ReadFromJsonAsync<List<CatalogModel>>() ?? new List<CatalogModel>());
	}

	public async Task<CatalogModel?> GetCatalogAsync(string id)
	{
		ArgumentNullException.ThrowIfNull(id);
		var httpResponse = await _client.GetAsync($"/api/Catalogs/{id}");

		if (httpResponse.StatusCode == HttpStatusCode.OK)
		{
			return await httpResponse.Content.ReadFromJsonAsync<CatalogModel>();
		}
		return null;
	}
}