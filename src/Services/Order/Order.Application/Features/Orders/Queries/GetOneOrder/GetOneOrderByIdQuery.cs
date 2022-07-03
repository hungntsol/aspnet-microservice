using MediatR;

namespace Order.Application.Features.Orders.Queries.GetOneOrder;

public class GetOneOrderByIdQuery : IRequest<OrderViewModel>
{
    public int Id { get; init; }

    public GetOneOrderByIdQuery(int id)
    {
        Id = id;
    }
}