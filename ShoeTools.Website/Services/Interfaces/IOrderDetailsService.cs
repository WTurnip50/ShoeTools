using ShoeTools.Core.Dto;
using ShoeTools.Core.Http;

namespace ShoeTools.Website.Services.Interfaces;

public interface IOrderDetailsService
{
    Task<Response<List<OrderDetailsDto>>> GetAllDetails();
    Task<Response<OrderDetailsDto>> GetDetailsById(int id);
    Task<Response<OrderDetailsDto>> SaveAsync(OrderDetailsDto orderDetailsDto);
    Task<Response<OrderDetailsDto>> UpdateAsync(OrderDetailsDto orderDetailsDto);
    Task<Response<bool>> Delete(int id);
}