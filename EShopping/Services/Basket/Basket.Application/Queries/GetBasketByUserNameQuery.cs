using Basket.Application.Dto;
using MediatR;

namespace Basket.Application.Queries
{
    public class GetBasketByUserNameQuery: IRequest<ShoppingCartDto>
    {
       public GetBasketByUserNameQuery(string userName)
        {
            UserName = userName;
        }

        public string UserName { get; }
    }
}