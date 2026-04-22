using Dapper;
using Dapper.Contrib.Extensions;
using ShoeTools.Api.DataAccess.Interfaces;
using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Repositories;

public class OrdersRepository : IOrdersRepository
{
    private readonly IDBContext _dbContext;

    public OrdersRepository(IDBContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Orders>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Orders WHERE IsDeleted = 0";
        var orders = await _dbContext.Connection.QueryAsync<Orders>(sql);
        return orders.ToList();
    }

    public async Task<Orders> GetOrderById(int id)
    {
        var order = await _dbContext.Connection.GetAsync<Orders>(id);
        if (order == null)
        {
            return null;
        }

        return order.IsDeleted == true ? null : order;
    }

    public async Task<Orders> SaveAsync(Orders order)
    {
        order.Id = await _dbContext.Connection.InsertAsync(order);
        return order;
    }

    public async Task<Orders> UpdateAsync(Orders order)
    {
        await _dbContext.Connection.UpdateAsync(order);
        return order;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var order = await GetOrderById(id);
        if (order == null)
        {
            return false;
        }

        order.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(order);
    }
}