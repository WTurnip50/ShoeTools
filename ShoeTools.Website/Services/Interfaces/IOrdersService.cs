using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;

namespace ShoeTools.Website.Services.Interfaces;

public interface IOrdersService
{
    Task<Response<List<OrdersDto>>> GetAllOrders();
    Task<Response<OrdersDto>> GetOrderById(int id);
    Task<Response<OrdersDto>> SaveAsync(OrdersDto orderDetailsDto);
    Task<Response<OrdersDto>> UpdateAsync(OrdersDto orderDetailsDto);
    Task<Response<bool>> Delete(int id);
}