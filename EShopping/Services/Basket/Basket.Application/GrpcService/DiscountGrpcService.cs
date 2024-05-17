
using Discount.Grpc.Protos;


namespace Basket.Application.GrpcService
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient grpcClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient grpcClient)
        {
            this.grpcClient = grpcClient;

        }
        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest { ProductName = productName };
            return await grpcClient.GetDiscountAsync(discountRequest);
        }
    }
}
