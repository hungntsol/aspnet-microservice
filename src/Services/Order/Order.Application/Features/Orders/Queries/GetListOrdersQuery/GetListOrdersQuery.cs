using MediatR;
using Order.Application.Features.Orders.Queries.Vms;

namespace Order.Application.Features.Orders.Queries.GetListOrdersQuery;

public class GetListOrdersQuery : IRequest<List<OrderViewModel>>
{
    public string? Username { get; init; }

    public GetListOrdersQuery(string? username)
    {
        Username = username;
    }
}