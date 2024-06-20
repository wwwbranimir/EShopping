using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBusConsumer
{
    // IConsumer implaementation
    public class BasketOrderingConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator mediatr;
        private readonly IMapper mapper;
        private readonly ILogger<BasketOrderingConsumer> logger;

        public BasketOrderingConsumer(IMediator mediatr, IMapper mapper, ILogger<BasketOrderingConsumer> logger)
        {
            this.mediatr = mediatr;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            var command = mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await mediatr.Send(command);
            logger.LogInformation($"BasketCheckoutEvent consumed successfully. Created Order Id: {result}");


            
        }
    }
}