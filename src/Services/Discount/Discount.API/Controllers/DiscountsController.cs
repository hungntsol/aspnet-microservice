using System.Threading.Tasks;
using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountRepository _discountRepository;

        public DiscountsController(IDiscountRepository discountRepository)
        {
            _discountRepository = discountRepository;
        }

        [HttpGet("{productName}")]
        public async Task<IActionResult> GetDiscount(string productName)
        {
            var data = await _discountRepository.GetDiscountAsync(productName);
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDiscount([FromBody] Coupon coupon)
        {
            var result = await _discountRepository.SaveDiscountAsync(coupon);
            return Ok(result);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateDiscount([FromBody] Coupon coupon)
        {
            var result = await _discountRepository.UpdateDiscountAsync(coupon);
            return Ok(result);
        }
        
        [HttpDelete("{productName}")]
        public async Task<IActionResult> DeleteDiscount(string productName)
        {
            var result = await _discountRepository.DeleteDiscountAsync(productName);
            return Ok(result);
        }
    }
}