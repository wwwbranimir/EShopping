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
        private readonly IPublishEndpoint publishEndPoint;
        private readonly ILogger<BasketController> logger;

        public BasketController(IMediator mediatr,  IPublishEndpoint publishEndPoint, ILogger<BasketController> logger)
        {
            this.mediatr = mediatr;
            this.publishEndPoint = publishEndPoint;
            this.logger = logger;
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

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
            var basket = await mediatr.Send(query);
           
            if (basket == null)
            {
                return BadRequest();
            }   
           var eventMessage = BasketMapper.MapperExtension.Map<BasketCheckoutEvent>(basketCheckout);
            logger.LogInformation("Event message is mapped from BasketCheckout");

            eventMessage.TotalPrice = basket.TotalPrice;
            await publishEndPoint.Publish(eventMessage);
            //log event message being published to the event bus
            logger.LogInformation($"Event message {eventMessage} is published to the event bus");


            //remove the basket
            await mediatr.Send(new DeleteBasketByUserNameCommand(basketCheckout.UserName));
            logger.LogInformation("Basket is deleted after checkout");
            return Accepted();
        }
       
    }
}
