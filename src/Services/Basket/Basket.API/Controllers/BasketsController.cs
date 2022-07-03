using Basket.API.Entities;
using Basket.API.GrpcServices;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;
    private readonly DiscountGrpcService _discountGrpcService;
    
    public BasketsController(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
    {
        _basketRepository = basketRepository;
        _discountGrpcService = discountGrpcService;
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
}