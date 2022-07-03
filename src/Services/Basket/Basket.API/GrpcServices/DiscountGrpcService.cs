using Discount.Grpc.Protos;

namespace Basket.API.GrpcServices;

public class DiscountGrpcService
{
    private readonly DiscountService.DiscountServiceClient _discountServiceClient;
    
    public DiscountGrpcService(DiscountService.DiscountServiceClient discountServiceClient)
    {
        _discountServiceClient = discountServiceClient;
    }

    public async Task<CouponModel> GetDiscount(string productName)
    {
        var discountRequest = new GetDiscountRequest() { ProductName = productName };
        var discountResponse = await _discountServiceClient.GetDiscountAsync(discountRequest);
        return discountResponse;
    }
}