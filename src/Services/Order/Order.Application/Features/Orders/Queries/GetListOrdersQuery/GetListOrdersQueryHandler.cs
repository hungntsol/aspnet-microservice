using MapsterMapper;
using MediatR;
using Order.Application.Contracts.Persistence;
using Order.Application.Features.Orders.Queries.Vms;

namespace Order.Application.Features.Orders.Queries.GetListOrdersQuery;

public class GetListOrdersQueryHandler : IRequestHandler<GetListOrdersQuery, List<OrderViewModel>>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    public GetListOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    public async Task<List<OrderViewModel>> Handle(GetListOrdersQuery request, CancellationToken cancellationToken)
    {
        var dataList = await _orderRepository.GetOrdersByUsername(request.Username);
        var dataListViewModel = _mapper.Map<List<OrderViewModel>>(dataList);
        return dataListViewModel;
    }
}