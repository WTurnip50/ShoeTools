using ShoeTools.Core.Dto;

namespace ShoeTools.Api.Services.Interfaces;

public interface IOrderService
{
    Task<bool> OrderExists(int orderId);
    
    Task<OrdersDto> SaveAsync(OrdersDto ordersDto);
    
    Task<OrdersDto> UpdateAsync(OrdersDto ordersDto);
    
    Task<List<OrdersDto>> GetAllOrders();
    
    Task<bool> DeleteAsync(int orderId);
    
    Task<OrdersDto> GetById(int orderId);
}