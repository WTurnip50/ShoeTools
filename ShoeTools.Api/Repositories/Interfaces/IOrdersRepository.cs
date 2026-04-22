using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories.Interfaces;

public interface IOrdersRepository
{
    Task<List<Orders>> GetAllAsync();
    Task<Orders> GetOrderById(int id);
    Task <Orders> SaveAsync(Orders order);
    Task<Orders> UpdateAsync(Orders order);
    Task<bool> DeleteAsync(int id);
}