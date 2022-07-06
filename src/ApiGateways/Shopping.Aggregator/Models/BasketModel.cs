using System.Text.Json.Serialization;

namespace Shopping.Aggregator.Models;

public class BasketModel
{
	public string Username { get; set; } = null!;
	[JsonPropertyName("cartItems")] 
	public IList<BasketItemModel> Items { get; set; } = new List<BasketItemModel>();
	public decimal TotalPrice { get; set; }
}