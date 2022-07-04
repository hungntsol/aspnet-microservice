#nullable disable

using MediatR;
using Order.Application.Features.Orders.Commands.Checkout;
using Order.Application.Mapping;

namespace Order.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommand : CheckoutOrderCommand, IRequest<Unit>
{
    public int Id { get; init; }
    
    // Remain use the same schema as the Checkout command
}