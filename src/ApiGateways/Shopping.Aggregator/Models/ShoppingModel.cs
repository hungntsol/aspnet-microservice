namespace Shopping.Aggregator.Models;

public class ShoppingModel
{
	public string Username { get; set; } = null!;
	public BasketModel? Basket { get; set; }
	public IEnumerable<OrderResponseModel>? Orders { get; set; }
}