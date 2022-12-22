namespace TwistFood.Api.Models;

public class Discount
{
    public int Id { get; set; }
    public string DiscountName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime StartTime { get; set;}
    public DateTime EndTime { get; set;}
    public double Price { get; set;}    
}
