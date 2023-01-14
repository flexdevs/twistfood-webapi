using OnlineMarket.Domain.Common;

namespace TwistFood.Domain.Entities.Categories;

public class Category : BaseEntity
{
    public string CategoryName { get; set; } = string.Empty;
    
}
