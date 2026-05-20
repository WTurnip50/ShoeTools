using ShoeTools.Core.Entities;

namespace ShoeTools.Core.Dto;

public class OrderDetailsDto : DtoBase
{
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    public OrderDetailsDto()
    {
    }

    public OrderDetailsDto(OrderDetails orderDetails)
    {
        Id = orderDetails.Id;
        OrderID = orderDetails.OrderID;
        ProductID = orderDetails.ProductID;
        UnitPrice = orderDetails.UnitPrice;
        Quantity = orderDetails.Quantity;
    }
}