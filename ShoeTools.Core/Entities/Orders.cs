namespace ShoeTools.Core.Entities;

public class Orders : EntityBase
{
    public int IdClient { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal Total { get; set; }
    public string PaymentMethod { get; set; }
    public string PaymentStatus { get; set; }
}