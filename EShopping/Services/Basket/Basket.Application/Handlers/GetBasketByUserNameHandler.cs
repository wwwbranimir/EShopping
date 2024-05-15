using Basket.Application.Dto;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Core.Repositories;
using MediatR;
namespace Basket.Application.Handlers
{
    public class GetBasketByUserNameHandler : IRequestHandler<GetBasketByUserNameQuery, ShoppingCartDto>
    {
        private readonly IBasketRepository basketRepository;

        public GetBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public async Task<ShoppingCartDto> Handle(GetBasketByUserNameQuery request, CancellationToken cancellationToken)
        {
            var shoppingCart = await basketRepository.GetBasket(request.UserName);
            var shoppingCartDto = BasketMapper.MapperExtension.Map<ShoppingCartDto>(shoppingCart);
            return shoppingCartDto;
        }
    }
}
