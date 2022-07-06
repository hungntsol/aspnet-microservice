using EventBus.Message.Events;
using MapsterMapper;
using MassTransit;
using MediatR;
using Order.Application.Features.Orders.Commands.Checkout;

namespace Order.API.EventBusConsumers;

public class BasketCheckoutConsumer : IConsumer<BasketCheckoutEvent>
{
	private readonly IMapper _mapper;
	private readonly ILogger<BasketCheckoutConsumer> _logger;
	private readonly IMediator _mediator;

	public BasketCheckoutConsumer(IMapper mapper, ILogger<BasketCheckoutConsumer> logger, IMediator mediator)
	{
		_mapper = mapper;
		_logger = logger;
		_mediator = mediator;
	}

	public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
	{
		var command = _mapper.Map<CheckoutOrderCommand>(context.Message);
		var response = await _mediator.Send(command);

		if (response > 0)
		{
			_logger.LogInformation("BasketCheckoutEvent: consume successfully. Order id: {0}", response);
		}
	}
}