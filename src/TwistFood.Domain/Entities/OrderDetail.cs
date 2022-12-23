namespace TwistFood.Api.Models;

public class OrderDetail
{
    public long Id { get; set; }

    public long OrderId { get; set; }
    public virtual Order Order { get; set; }    
    public long ProductId { get; set; } 
    public virtual Product Product { get; set; }
    public int Amount { get; set; } 
    public double Price { get; set; }   
}
