using Basket.Core.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basket.Core.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache distributedCache;

        public BasketRepository(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public async Task<bool> DeleteBasket(string userName)
        {
            await distributedCache.RemoveAsync(userName);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await distributedCache.GetStringAsync(userName);
            if (basket is null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await distributedCache.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));
            return await GetBasket(basket.UserName);
        }
    }
}
