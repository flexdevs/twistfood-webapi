using OnlineMarket.Domain.Common;
using TwistFood.Domain.Common;
using TwistFood.Domain.Entities.Discounts;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Users;
using TwistFood.Domain.Enums;

namespace TwistFood.Domain.Entities.Order;

public class Order : Auditable
{
    public long UserId { get; set; }
    public virtual User User { get; set; } = default!;
    public long? DeliverId { get; set; }
    public virtual Deliver? Deliver { get; set; }
    public long ILocationId { get; set; }
    public virtual Location ILocation { get; set; } = default!;
    public double TotalSum { get; set; }
    public PaymentType PaymentType { get; set; } = PaymentType.Cash;
    public OrderStatus Status { get; set; }
    public double DeleviryPrice { get; set; }
    public bool IsDiscount { get; set; }
    public long? DiscountId { get; set; }
    public virtual Discount? Discount { get; set; }
    public long? OperatorId { get; set; }
    public virtual Operator? Operator { get; set; }
    public DateTime? DeliveredAt { get; set; }

}
