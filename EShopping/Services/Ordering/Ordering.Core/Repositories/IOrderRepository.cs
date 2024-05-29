using Ordering.Core.Entities;

namespace Ordering.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersByUserName(string userName);
 
        Task<Order> AddAsync(Order order);
        Task<Order> GetByIdAsync(int id);

        Task<Order> UpdateAsync(Order order);
        Task<Order> DeleteAsync(Order order);
    }
}
