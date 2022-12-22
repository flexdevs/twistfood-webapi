using TwistFood.Api.Common.Enums;

namespace TwistFood.Api.Models;

public class Order
{
    public long Id { get; set; }    
    public long UserId { get; set; }    
    public virtual User User { get; set; }
    public int DeliverId { get; set; }  
    public virtual Deliver Deliver { get; set; }    
    public long ILocationId { get; set; } 
    public virtual Location ILocation { get; set; }
    public double TotalSum { get; set; }    
    public DateTime CreateAt { get; set; }
    public PaymentType PaymentType { get; set; } = PaymentType.Cash;
    public string Status { get; set; }  = string.Empty;
    public double DeleviryPrice { get; set; }   
    public bool IsDiscount { get; set; }    
    public int DiscountId { get; set; } 
    public virtual Discount Discount { get; set; }
    public int OperatorId { get; set; }
    public virtual Operator Operator { get; set; }  

}
