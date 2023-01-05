using TwistFood.Domain.Common;

namespace TwistFood.Domain.Entities.Discounts;

public class Discount : Auditable
{
    public string DiscountName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public double Price { get; set; }
}
