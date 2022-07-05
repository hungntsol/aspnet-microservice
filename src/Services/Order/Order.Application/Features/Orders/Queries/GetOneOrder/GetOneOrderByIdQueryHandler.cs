using Mapster;
using MediatR;
using Order.Application.Contracts.Persistence;
using Order.Application.Features.Orders.Queries.Vms;

namespace Order.Application.Features.Orders.Queries.GetOneOrder;

public class GetOneOrderByIdQueryHandler : IRequestHandler<GetOneOrderByIdQuery, OrderViewModel?>
{
    private readonly IOrderRepository _orderRepository;

    public GetOneOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderViewModel?> Handle(GetOneOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _orderRepository.GetByIdAsync(request.Id);

        if (data is null)
            return null;
        
        var mapData = data!.Adapt<OrderViewModel>();
        return mapData;
    }
}