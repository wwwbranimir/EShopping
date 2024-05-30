using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Application.Extensions;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository orderRepository;
        private readonly ILogger<DeleteOrderCommandHandler> logger;

       
        public DeleteOrderCommandHandler(IOrderRepository orderRepository, ILogger<DeleteOrderCommandHandler> logger)
        {
            this.orderRepository = orderRepository;
            this.logger = logger;
        }
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            //get order by id
            var orderEntity = await orderRepository.GetByIdAsync(request.Id);
            //if order is null
            if (orderEntity == null)
            {
                //throw exception
                throw new OrderNotFoundException("Order not found.");
            }
            //delete order
            await orderRepository.DeleteAsync(orderEntity);
            //log
            logger.LogInformation($"Order {orderEntity.Id} is successfully deleted.");
          
           
        }
    }
}
