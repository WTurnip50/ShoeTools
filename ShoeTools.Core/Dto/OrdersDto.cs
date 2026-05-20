using ShoeTools.Core.Entities;

namespace ShoeTools.Core.Dto;

public class OrdersDto : DtoBase
{
    public int ClientId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Total { get; set; }
    public string PaymentMethod { get; set; }
    public bool PaymentStatus { get; set; }

    public OrdersDto()
    {
    }

    public OrdersDto(Orders order)
    {
        ClientId = order.ClientId;
        OrderDate = order.OrderDate;
        Total = order.Total;
        PaymentMethod = order.PaymentMethod;
        PaymentStatus = order.PaymentStatus;
    }
}