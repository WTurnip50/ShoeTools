using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories.Interfaces;

public interface IOrderDetailsRepository
{
    Task<List<OrderDetails>> GetOrders();
    Task<OrderDetails> GetOrderItemById(int id);
    Task <OrderDetails> SaveAsync(OrderDetails order);
    Task<OrderDetails> UpdateAsync(OrderDetails order);
    Task<bool> DeleteAsync(int id);
}