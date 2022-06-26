using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketsController : ControllerBase
{
    private readonly IBasketRepository _basketRepository;

    public BasketsController(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
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
        return Ok(await _basketRepository.UpdateBasketAsync(basket));
    }

    [HttpDelete("{userName}")]
    public async Task<IActionResult> DeleteBasket(string userName)
    {
        return Ok(await _basketRepository.DeleteBasketAsync(userName));
    }
}