namespace TwistFood.Api.Models;

public class OrderDetail
{
    public long Id { get; set; }

    public long OrderId { get; set; }
    public virtual Order Order { get; set; } = default!;
    public long ProductId { get; set; } 
    public virtual Product Product { get; set; } = default!;
    public int Amount { get; set; } 
    public double Price { get; set; }   
}
