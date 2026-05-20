using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Services.Interfaces;

public class OrderDetailsService : IOrderDetailsService
{
    private readonly IOrderDetailsRepository _orderDetailsRepository;

    public OrderDetailsService(IOrderDetailsRepository orderDetailsRepository)
    {
        _orderDetailsRepository = orderDetailsRepository;
    }
    public async Task<bool> OrderExists(int orderId)
    {
        var order = await _orderDetailsRepository.GetOrderItemById(orderId);
        return order != null;
    }

    public async Task<OrderDetailsDto> SaveAsync(OrderDetailsDto detailsDto)
    {
        var order = new OrderDetails
        {
            OrderID = detailsDto.OrderID,
            ProductID = detailsDto.ProductID,
            UnitPrice = detailsDto.UnitPrice,
            Quantity = detailsDto.Quantity,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate = DateTime.Now
        };
        order = await _orderDetailsRepository.SaveAsync(order);
        order.Id = detailsDto.OrderID;
        return detailsDto;
    }

    public async Task<OrderDetailsDto> UpdateAsync(OrderDetailsDto detailsDto)
    {
        var order = await _orderDetailsRepository.GetOrderItemById(detailsDto.OrderID);
        if (order != null)
        {
            throw new Exception("Order Not Found");
        }
        order.OrderID = detailsDto.OrderID;
        order.ProductID = detailsDto.ProductID;
        order.UnitPrice = detailsDto.UnitPrice;
        order.Quantity = detailsDto.Quantity;
        order.UpdatedBy = "";
        order.UpdatedDate = DateTime.Now;
        await _orderDetailsRepository.UpdateAsync(order);
        return detailsDto;
    }

    public async Task<List<OrderDetailsDto>> GetAllOrders()
    {
        var orderDetails = await _orderDetailsRepository.GetOrders();
        var detailsDto = orderDetails.Select(order => new OrderDetailsDto(order)).ToList();
        return detailsDto;
    }

    public async Task<bool> DeleteAsync(int orderId)
    {
        return await _orderDetailsRepository.DeleteAsync(orderId);
    }

    public async Task<OrderDetailsDto> GetById(int orderId)
    {
        var details = await _orderDetailsRepository.GetOrderItemById(orderId);
        if (details == null)
        {
            throw new Exception("Order Not Found");
        }
        var  detailsDto = new OrderDetailsDto(details);
        return detailsDto;
    }
}