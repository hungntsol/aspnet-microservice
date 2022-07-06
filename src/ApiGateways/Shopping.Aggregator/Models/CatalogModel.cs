namespace Shopping.Aggregator.Models;

public class CatalogModel
{
	public string Id { get; set; } = null!;
	public string Name { get; set; } = null!;
	public string? Category { get; set; }
	public string? Summary { get; set; }
	public string? Description { get; set; }
	public string? Image { get; set; }
	public decimal Price { get; set; }
}