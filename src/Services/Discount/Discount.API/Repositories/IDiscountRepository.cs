using Discount.API.Entities;

namespace Discount.API.Repositories;

public interface IDiscountRepository
{
    Task<Coupon> GetDiscountAsync(string productName);
    Task<bool> SaveDiscountAsync(Coupon coupon);
    Task<bool> UpdateDiscountAsync(Coupon coupon);
    Task<bool> DeleteDiscountAsync(string productName);
}