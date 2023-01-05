using OnlineMarket.Domain.Common;
using TwistFood.Domain.Common;
using TwistFood.Domain.Entities.Categories;

namespace TwistFood.Domain.Entities.Products;

public class Product : Auditable
{
    public long CategoryId { get; set; }
    public virtual Category Category { get; set; } = default!;
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public string ImagePath { get; set; } = string.Empty;
    public double Price { get; set; }

}
