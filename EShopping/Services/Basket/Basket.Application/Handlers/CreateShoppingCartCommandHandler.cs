using Basket.Application.Commands;
using Basket.Application.Dto;
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

        public CreateShoppingCartCommandHandler(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public async Task<ShoppingCartDto> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
        {
            //TODO: call discount service here and apply discount to the items
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
