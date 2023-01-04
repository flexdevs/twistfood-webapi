using OnlineMarket.Domain.Common;
using TwistFood.Domain.Enums;

namespace TwistFood.Api.Models;

public class Order : BaseEntity
{ 
    public long UserId { get; set; }    
    public virtual User User { get; set; } = default!;
    public int DeliverId { get; set; }  
    public virtual Deliver Deliver { get; set; } = default!;    
    public long ILocationId { get; set; } 
    public virtual Location ILocation { get; set; } = default!;
    public double TotalSum { get; set; }    
    public DateTime CreateAt { get; set; }
    public PaymentType PaymentType { get; set; } = PaymentType.Cash;
    public string Status { get; set; }  = string.Empty;
    public double DeleviryPrice { get; set; }   
    public bool IsDiscount { get; set; }    
    public int DiscountId { get; set; }
    public virtual Discount Discount { get; set; } = default!;
    public int OperatorId { get; set; }
    public virtual Operator Operator { get; set; } = default!;

}
