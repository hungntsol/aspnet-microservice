using System.ComponentModel.DataAnnotations.Schema;
using Order.Domain.Entities.Common;

namespace Order.Domain.Entities;

public class Order : EntityBase
{
    public string? Username { get; set; }
    [Column(TypeName = "decimal(18,2)")]
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