using Discount.Application.Queries;
using Discount.Core.Repositories;
using Discount.Grpc.Protos;
using Grpc.Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Application.Handlers
{
    public class GetDiscountQueryHandler : IRequestHandler<GetDiscountQuery, CouponModel>
    {
        private readonly IDiscountRepository repository;

        public GetDiscountQueryHandler(IDiscountRepository repository)
        {
            this.repository = repository;
        }
        public async Task<CouponModel> Handle(GetDiscountQuery request, CancellationToken cancellationToken)
        {
            var coupon = await repository.GetDiscount(request.ProductName);
            if (coupon == null)
            {
                throw new RpcException(
                    new Status(StatusCode.NotFound,$"Discount with the product name {request.ProductName} was not found."));
            }
            //TODO: implement automapper extension in separate class

            return new CouponModel
            {
                Id = coupon.Id,
                Amount = coupon.Amount,
                Description = coupon.Description,
                ProductName = coupon.ProductName
            };
        }
    }
}
