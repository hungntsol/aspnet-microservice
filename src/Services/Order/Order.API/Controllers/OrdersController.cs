using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders.Commands.Checkout;
using Order.Application.Features.Orders.Commands.DeleteOrder;
using Order.Application.Features.Orders.Commands.UpdateOrder;
using Order.Application.Features.Orders.Queries.GetListOrdersQuery;
using Order.Application.Features.Orders.Queries.GetOneOrder;
using Order.Application.Features.Orders.Queries.Vms;

namespace Order.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(OrderViewModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrder(int id)
    {
        var data = await _mediator.Send(new GetOneOrderByIdQuery(id));
        return Ok(data);
    }


    [HttpGet("{username}")]
    [ProducesResponseType(typeof(IEnumerable<OrderViewModel>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetOrders(string username)
    {
        var data = await _mediator.Send(new GetListOrdersQuery(username));
        return Ok(data);
    }

    [HttpPost("checkout")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Checkout([FromBody] CheckoutOrderCommand command)
    {
        var data = await _mediator.Send(command);
        return Created("", "");
    }

    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand command)
    {
        var data = await _mediator.Send(command);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteOrder(int id)
    {
        var data = await _mediator.Send(new DeleteOrderCommand() { Id = id });
        return Ok();
    }
}