using Discount.Application.Commands;
using Discount.Application.Queries;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;

namespace Discount.API.Services
{
    public class DiscountService:DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMediator mediator;
        private readonly ILogger<DiscountService> logger;

        public DiscountService(IMediator mediator, ILogger<DiscountService> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        

        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await mediator.Send(new GetDiscountQuery(request.ProductName));
            logger.LogInformation("Discount is retrieved for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
            return coupon;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
           var command = new CreateDiscountCommand(request.Coupon.ProductName, request.Coupon.Description, request.Coupon.Amount);
            var coupon = await mediator.Send(command);
            logger.LogInformation("Discount is successfully created for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
            return coupon;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var command = new UpdateDiscountCommand(request.Coupon.Id, request.Coupon.ProductName, request.Coupon.Description,request.Coupon.Amount);
            var coupon = await mediator.Send(command);
            logger.LogInformation("Discount is successfully updated for ProductName : {ProductName}, Amount : {Amount}", coupon.ProductName, coupon.Amount);
            return coupon;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await mediator.Send(new DeleteDiscountCommand(request.ProductName));
            var response = new DeleteDiscountResponse
            {
                Success = deleted
            };
            return response;
        }
    }
}
