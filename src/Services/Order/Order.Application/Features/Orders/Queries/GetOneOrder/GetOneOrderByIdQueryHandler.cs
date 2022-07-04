using MapsterMapper;
using MediatR;
using Order.Application.Contracts.Persistence;
using Order.Application.Features.Orders.Queries.Vms;

namespace Order.Application.Features.Orders.Queries.GetOneOrder;

public class GetOneOrderByIdQueryHandler : IRequestHandler<GetOneOrderByIdQuery, OrderViewModel>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetOneOrderByIdQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<OrderViewModel> Handle(GetOneOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _orderRepository.GetByIdAsync(request.Id);
        var mapData = _mapper.Map<OrderViewModel>(data);
        return mapData;
    }
}