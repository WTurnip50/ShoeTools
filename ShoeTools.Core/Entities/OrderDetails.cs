namespace ShoeTools.Core.Entities;

public class OrderDetails : EntityBase
{
    public int OrderID { get; set; }
    public int ProductID { get; set; }
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}