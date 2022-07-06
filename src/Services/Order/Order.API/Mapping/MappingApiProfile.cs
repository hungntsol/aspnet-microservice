using EventBus.Message.Events;
using Mapster;
using Order.Application.Features.Orders.Commands.Checkout;

namespace Order.API.Mapping;

public class MappingApiProfile : IRegister
{
	public void Register(TypeAdapterConfig config)
	{
		config.NewConfig<CheckoutOrderCommand, BasketCheckoutEvent>();
		config.NewConfig<BasketCheckoutEvent, CheckoutOrderCommand>();
	}
}