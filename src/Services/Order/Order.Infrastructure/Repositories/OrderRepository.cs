using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Persistence;
using Order.Infrastructure.Persistence;

namespace Order.Infrastructure.Repositories;

public class OrderRepository : AsyncRepositoryBase<Domain.Entities.Order>, IOrderRepository
{
	public OrderRepository(OrderDataContext context) : base(context)
	{
	}

	public async Task<IEnumerable<Domain.Entities.Order>> GetOrdersByUsername(string username)
	{
		var data = await Context.Set<Domain.Entities.Order>()
			.Where(x => x.Username == username)
			.ToListAsync();

		return data;
	}
}