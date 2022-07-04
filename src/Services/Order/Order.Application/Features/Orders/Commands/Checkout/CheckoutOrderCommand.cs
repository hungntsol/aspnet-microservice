#nullable disable

using FluentValidation;
using MediatR;
using Order.Application.Mapping;

namespace Order.Application.Features.Orders.Commands.Checkout;

public class CheckoutOrderCommand : IRequest<int> 
{
    public string Username { get; init; }
    public decimal TotalPrice { get; init; }
    
    // Billing
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Address { get; init; }
    public string Email { get; init; }
    public string Phone { get; init; }
    public string Notes { get; init; }
    
    // Payment
    public string CardName { get; set; }
    public string CardNumber { get; set; }
    public string CardExpiration { get; set; }
    public string CVV { get; set; }
    public int PaymentMethod { get; set; }
}

public sealed class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
{
    public CheckoutOrderCommandValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .NotNull().WithMessage("Username is required");
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is invalid");
        RuleFor(x => x.TotalPrice)
            .GreaterThan(0).WithMessage("Total price is invalid");
    }
}