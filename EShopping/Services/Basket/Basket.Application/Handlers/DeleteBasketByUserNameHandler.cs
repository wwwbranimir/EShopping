using Basket.Application.Commands;
using Basket.Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Handlers
{
    public class DeleteBasketByUserNameHandler : IRequestHandler<DeleteBasketByUserNameCommand>
    {
        private readonly IBasketRepository basketRepository;

        public DeleteBasketByUserNameHandler(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }
        public async Task Handle(DeleteBasketByUserNameCommand request, CancellationToken cancellationToken)
        {
            await basketRepository.DeleteBasket(request.UserName);

            
        }
    }
}
