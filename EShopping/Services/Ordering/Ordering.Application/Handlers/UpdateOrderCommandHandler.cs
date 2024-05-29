using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.DTO;
using Ordering.Application.Extensions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateOrderCommandHandler> logger;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository, IMapper mapper, ILogger<UpdateOrderCommandHandler> looger)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
            this.logger = looger;
        }
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = await orderRepository.GetByIdAsync(request.Id);
            if (orderEntity == null)
            {
                throw new OrderNotFoundException($"Order with id {request.Id} not found.");
            }
            mapper.Map(request, orderEntity, typeof(UpdateOrderCommand), typeof(Order));
           
            await orderRepository.UpdateAsync(orderEntity);
            logger.LogInformation($"Order {orderEntity.Id} is successfully updated.");
            
        }
    }
}
