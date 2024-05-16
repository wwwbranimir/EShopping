using Basket.Application.Commands;
using Basket.Application.Dto;
using Basket.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
   
    public class BasketController : ApiController
    {
        private readonly IMediator mediatr;

        public BasketController(IMediator mediatr)
        {
            this.mediatr = mediatr;
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

       
    }
}
