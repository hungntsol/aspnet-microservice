using Mapster;
using Microsoft.AspNetCore.Mvc;
using Shopping.Aggregator.Models;
using Shopping.Aggregator.Services;

namespace Shopping.Aggregator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingController : ControllerBase
{
	private readonly IOrderService _orderService;
	private readonly ICatalogService _catalogService;
	private readonly IBasketService _basketService;

	public ShoppingController(IOrderService orderService, ICatalogService catalogService, IBasketService basketService)
	{
		_orderService = orderService;
		_catalogService = catalogService;
		_basketService = basketService;
	}

	[HttpGet("{username}")]
	[ProducesResponseType(typeof(ShoppingModel), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetShopping(string username)
	{
		var shoppingBasket = await _basketService.GetBasketAsync(username);

		if (shoppingBasket is null)
			return Ok();
		
		foreach (var item in shoppingBasket.Items)
		{
			var product = await _catalogService.GetCatalogAsync(item.ProductId);
			if (product is null) continue;

			item.Price = product.Price;
			item.ProductName = product.Name;
		}

		var orders = await _orderService.GetEnumerableOrdersAsync(username);
		var shoppingModel = new ShoppingModel
		{
			Basket = shoppingBasket,
			Orders = orders,
			Username = username
		};

		return Ok(shoppingModel);
	}
}