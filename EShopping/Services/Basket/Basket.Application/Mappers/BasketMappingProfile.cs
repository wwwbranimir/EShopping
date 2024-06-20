using AutoMapper;
using Basket.Application.Dto;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Mappers
{
    public class BasketMappingProfile:Profile
    {
        public BasketMappingProfile()
        {
            CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap();
            CreateMap<ShoppingCartItem, ShoppingCartItemDto>().ReverseMap();
            CreateMap<BasketCheckout, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
