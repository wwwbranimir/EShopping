using Basket.Application.Dto;
using Basket.Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Commands
{
    public class CreateShoppingCartCommand : IRequest<ShoppingCartDto>
    {
       public string UserName { get; set; }
        public List<ShoppingCartItem> Items { get; set; }

        public CreateShoppingCartCommand(string userName, List<ShoppingCartItem> items)
        {
            this.UserName = userName;
            Items = items;
        }
    }
}
