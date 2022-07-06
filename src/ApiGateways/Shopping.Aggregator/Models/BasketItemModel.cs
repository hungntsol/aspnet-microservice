namespace Shopping.Aggregator.Models;

public class BasketItemModel
{
	public string ProductId { get; set; } = null!;
	public string ProductName { get; set; } = null!;
	public int Quantity { get; set; }
	public decimal Price { get; set; }
}