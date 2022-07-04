using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Persistence;
using Order.Application.Exceptions;

namespace Order.Application.Features.Orders.Commands.DeleteOrder;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly Logger<DeleteOrderCommandHandler> _logger;
    
    public DeleteOrderCommandHandler(IOrderRepository orderRepository, Logger<DeleteOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _logger = logger;
    }

    public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);
        if (order is null)
        {
            _logger.LogError("Order {OrderId} not found", request.Id);
            throw new DataNotFoundException();
        }

        var deleted = await _orderRepository.RemoveAsync(order);
        
        if (deleted) return Unit.Value;
        
        _logger.LogError("Order {OrderId} not deleted", request.Id);
        return Unit.Value;
    }
}