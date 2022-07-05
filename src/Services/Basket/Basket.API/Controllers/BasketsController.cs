using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using EventBus.Message.Events;
using Mapster;
using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public BasketsController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    [HttpGet("{userName}")]
    public async Task<IActionResult> GetBasket(string userName)
    {
        var data = await _basketRepository.GetBasketAsync(userName);
        return Ok(data);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
    {
        // Communicate with Discount Grpc service
        foreach (var item in basket.CartItems)
        {
            var coupon = await _discountGrpcService.GetDiscount(item.ProductName);
            
            // Calculate latest price of product and update the basket
            item.Price -= coupon.Amount;
        }
        
        // Consume Discount Grpc
        
        
        return Ok(await _basketRepository.UpdateBasketAsync(basket));
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        return Ok(await _basketRepository.DeleteBasketAsync(userName));
    }

    [HttpPost("checkout")]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CheckoutBasket([FromBody] BasketCheckout command)
    {
        // select existed basket
        var basket  = await _basketRepository.GetBasketAsync(command.Username);
        if (basket is null)
        {
            return BadRequest();
        }
        
        // send message to RabbitMQ
        var eventMessage = command.Adapt<BasketCheckoutEvent>();
        eventMessage.TotalPrice = basket.TotalPrice;
        await _publishEndpoint.Publish(eventMessage);

        // remove basket from repository

        return Accepted();
    }
}