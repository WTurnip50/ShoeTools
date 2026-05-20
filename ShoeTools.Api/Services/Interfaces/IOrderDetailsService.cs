using ShoeTools.Core.Dto;

namespace ShoeTools.Api.Services.Interfaces;

public interface IOrderDetailsService
{
    Task<bool> OrderExists(int orderId);
    
    Task<OrderDetailsDto> SaveAsync(OrderDetailsDto detailsDto);
    
    Task<OrderDetailsDto> UpdateAsync(OrderDetailsDto detailsDto);
    
    Task<List<OrderDetailsDto>> GetAllOrders();
    
    Task<bool> DeleteAsync(int orderId);
    
    Task<OrderDetailsDto> GetById(int orderId);
}