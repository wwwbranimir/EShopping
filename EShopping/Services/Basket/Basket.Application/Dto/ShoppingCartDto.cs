using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Application.Dto
{
    public class ShoppingCartDto
    {
        public string UserName { get; set; }
        public List<ShoppingCartItemDto> Items { get; set; }
        public ShoppingCartDto(string userName)
        {
            UserName = userName;
        }

        public decimal TotalPrice
        {
            get
            {
                return Items.Sum(x => x.Price * x.Quantity);
            }
        }
    }
}
