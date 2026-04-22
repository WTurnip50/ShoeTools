using Dapper;
using Dapper.Contrib.Extensions;
using ShoeTools.Api.DataAccess.Interfaces;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories;

public class OrderDetailsRepository : IOrderDetailsRepository
{
    
    private readonly IDBContext _dbContext;

    public OrderDetailsRepository(IDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<List<OrderDetails>> GetOrders()
    {
        const string sql = "SELECT * FROM OrderDetails WHERE IsDeleted = 0";
        var orders = await _dbContext.Connection.QueryAsync<OrderDetails>(sql);
        return orders.ToList();
    }

    public async Task<OrderDetails> GetOrderItemById(int id)
    {
        var order = await _dbContext.Connection.GetAsync<OrderDetails>(id);
        if (order == null)
        {
            return null;
        }
        return order.IsDeleted == true ? null : order;
    }


    public async Task<OrderDetails> SaveAsync(OrderDetails order)
    {
        order.Id = await _dbContext.Connection.InsertAsync(order);
        return order;
    }

    public async Task<OrderDetails> UpdateAsync(OrderDetails order)
    {
        await _dbContext.Connection.UpdateAsync(order);
        return order;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var orderItem = await GetOrderItemById(id);
        if (orderItem == null)
        {
            return false;
        }

        orderItem.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(orderItem);
    }
}