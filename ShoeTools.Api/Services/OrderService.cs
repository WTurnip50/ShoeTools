using ShoeTools.Api.Repositories.Interfaces;
using ShoeTools.Api.Services.Interfaces;
using ShoeTools.Core.Dto;
using ShoeTools.Core.Entities;

namespace ShoeTools.Api.Services;

public class OrderService : IOrderService
{
    private readonly IOrdersRepository _orderRepository;

    public OrderService(IOrdersRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> OrderExists(int orderId)
    {
        var order = await _orderRepository.GetOrderById(orderId);
        return order != null;
    }

    public async Task<OrdersDto> SaveAsync(OrdersDto ordersDto)
    {
        var order = new Orders
        {
            ClientId =  ordersDto.ClientId,
            Total = ordersDto.Total,
            OrderDate =  ordersDto.OrderDate,
            PaymentMethod =  ordersDto.PaymentMethod,
            PaymentStatus = ordersDto.PaymentStatus,
            CreatedBy = "",
            CreatedDate = DateTime.Now,
            UpdatedBy = "",
            UpdatedDate =  DateTime.Now
        };
        order = await _orderRepository.SaveAsync(order);
        order.Id = order.Id;
        return  ordersDto;
    }

    public async Task<OrdersDto> UpdateAsync(OrdersDto ordersDto)
    {
        var order = await _orderRepository.GetOrderById(ordersDto.Id);
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        order.ClientId = ordersDto.ClientId;
        order.Total = ordersDto.Total;
        order.OrderDate = ordersDto.OrderDate;
        order.PaymentMethod = ordersDto.PaymentMethod;
        order.PaymentStatus = ordersDto.PaymentStatus;
        order.UpdatedBy = "";
        order.UpdatedDate = DateTime.Now;
        
        await _orderRepository.UpdateAsync(order);
        return ordersDto;
    }

    public async Task<List<OrdersDto>> GetAllOrders()
    {
        var orders = await _orderRepository.GetAllAsync();
        var ordersDto = orders.Select(order => new OrdersDto(order)).ToList();
        return ordersDto;
    }

    public async Task<bool> DeleteAsync(int orderId)
    {
        return await _orderRepository.DeleteAsync(orderId);
    }

    public async Task<OrdersDto> GetById(int orderId)
    {
        var order = await _orderRepository.GetOrderById(orderId);
        if (order == null)
        {
            throw new Exception("Order not found");
        }
        var orderDto = new OrdersDto(order);
        return orderDto;
    }
}