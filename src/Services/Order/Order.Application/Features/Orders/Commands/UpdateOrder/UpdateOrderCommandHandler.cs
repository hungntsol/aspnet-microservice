using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Persistence;
using Order.Application.Exceptions;

namespace Order.Application.Features.Orders.Commands.UpdateOrder;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<UpdateOrderCommandHandler> _logger;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper,
        ILogger<UpdateOrderCommandHandler> logger)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.Id);

        if (order is null)
        {
            _logger.LogError("Order {OrderId} not found", request.Id);
            throw new DataNotFoundException();
        }

        _mapper.Map(request, order, typeof(UpdateOrderCommand), typeof(Domain.Entities.Order));

        var updated = await _orderRepository.UpdateAsync(order);

        if (updated) return Unit.Value;

        _logger.LogError("Order {OrderId} not updated", request.Id);
        return Unit.Value;
    }
}