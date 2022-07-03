using Order.Application.Mapping;

namespace Order.Application.Features.Orders.Queries.GetOneOrder;

public class OrderViewModel : MappingBase<OrderViewModel, Domain.Entities.Order>
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public decimal TotalPrice { get; set; }
    // Bill info
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Note { get; set; }
    // Payment
    public string? CardName { get; set; }
    public string? CardNumber { get; set; }
    public string? CardExpiration { get; set; }
    public string? CVV { get; set; }
    public int PaymentMethod { get; set; }
}