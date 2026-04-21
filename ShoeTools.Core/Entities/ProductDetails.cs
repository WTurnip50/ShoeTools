namespace ShoeTools.Core.Entities;

public class ProductDetails : EntityBase
{
    public int IdProduct { get; set; }
    public string Model { get; set; }
    public int Size { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}