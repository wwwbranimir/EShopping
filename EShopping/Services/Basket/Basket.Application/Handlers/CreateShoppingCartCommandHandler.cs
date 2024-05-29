using Basket.Application.Commands;
using Basket.Application.Dto;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers
{
    public class CreateShoppingCartCommandHandler : IRequestHandler<CreateShoppingCartCommand, ShoppingCartDto>
    {
        private readonly IBasketRepository basketRepository;
        private readonly DiscountGrpcService discountGrpcService;

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            this.basketRepository = basketRepository;
            this.discountGrpcService = discountGrpcService;
        }
        public async Task<ShoppingCartDto> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //TODO: call discount service here and apply discount to the items

            foreach (var item in request.Items)
            {
                var coupon = await discountGrpcService.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }
            var shoppingCart = await basketRepository.UpdateBasket(new ShoppingCart
            {
                UserName = request.UserName,
                Items = request.Items
            });
            var shoppingCartDto = BasketMapper.MapperExtension.Map<ShoppingCartDto>(shoppingCart);
            return shoppingCartDto;
        }
    }
}
