using Basket.Application.Commands;
using Basket.Application.Dto;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{
   
    public class BasketController : ApiController
    {
        private readonly IMediator mediatr;
        private readonly DiscountGrpcService discountgrpcService;
        private readonly IPublishEndpoint publishEndPoint;

        public BasketController(IMediator mediatr, DiscountGrpcService grpcService, IPublishEndpoint publishEndPoint)
        {
            this.mediatr = mediatr;
            this.discountgrpcService = grpcService;
            this.publishEndPoint = publishEndPoint;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name="GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartDto>> GetBasketByUserName(string userName)
        {
            var basket = await mediatr.Send(new GetBasketByUserNameQuery(userName));
            return Ok(basket);
        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ShoppingCartDto>> UpdateBasket([FromBody] CreateShoppingCartCommand command)
        {
            var basket = await mediatr.Send(command);
            return Ok(basket);
          
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        public async Task DeleteBasketByUserName(string userName)
        {
            await mediatr.Send(new DeleteBasketByUserNameCommand(userName));
            
        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
            var basket = await mediatr.Send(query);

            if(basket == null)
            {
                return BadRequest();
            }   
           var eventMessage = BasketMapper.MapperExtension.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndPoint.Publish(eventMessage);
            //remove the basket
            await mediatr.Send(new DeleteBasketByUserNameCommand(basketCheckout.UserName));
            return Accepted();
        }
       
    }
}
