namespace Basket.API.Entities;

public class BasketCheckout
{
	public string Username { get; set; } = null!;
	public decimal TotalPrice { get; set; }
	
	// Billing
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
	public string? Email { get; set; }
	public string? Address { get; set; }
	public string? Phone { get; set; }
	public string? Note { get; set; }
	
	// Payment
	public string? CardName { get; set; }
	public string? CardNumber { get; set; }
	public string? CVV { get; set; }
	public int PaymentMethod { get; set; }
}