using AutoMapper;
using Discount.Grpc.Entities;
using Discount.Grpc.Protos;
using Discount.Grpc.Repositories;
using Grpc.Core;

namespace Discount.Grpc.Services;

public class DiscountService : Protos.DiscountService.DiscountServiceBase
{
    private readonly IDiscountRepository _discountRepository;
    private readonly ILogger<DiscountService> _logger;
    private readonly IMapper _mapper;

    public DiscountService(IDiscountRepository discountRepository, ILogger<DiscountService> logger, IMapper mapper)
    {
        _discountRepository = discountRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _discountRepository.GetDiscountAsync(request.ProductName);
        if (coupon is null)
        {
            _logger.LogWarning("Coupon not found for product {ProductName}", request.ProductName);
            return null;
        }

        var couponModel = _mapper.Map<CouponModel>(coupon);

        return couponModel;
    }

    public override async Task<CouponModel> SaveDiscount(SaveDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);

        var success = await _discountRepository.SaveDiscountAsync(coupon);

        if (success)
        {
            _logger.LogInformation("[DONE] Save coupon {CouponId}", coupon.Id);
        }
        else
        {
            _logger.LogError("[ERROR] Save coupon {CouponId}", coupon.Id);
        }
        
        return request.Coupon;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var coupon = _mapper.Map<Coupon>(request.Coupon);
        var success = await _discountRepository.UpdateDiscountAsync(coupon);
        
        if (success)
        {
            _logger.LogInformation("[DONE] Update coupon {CouponId}", coupon.Id);
        }
        else
        {
            _logger.LogError("[ERROR] Update coupon {CouponId}", coupon.Id);
        }

        return request.Coupon;
    }

    public override async Task<AffectedResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var success = await _discountRepository.DeleteDiscountAsync(request.ProductName);
        
        if (success)
        {
            _logger.LogInformation("[DONE] Delete coupon {ProductName}", request.ProductName);
        }
        else
        {
            _logger.LogError("[ERROR] Delete coupon {ProductName}", request.ProductName);
        }
        
        var response = new AffectedResponse() {Affected = success};
        return response;
    }
}