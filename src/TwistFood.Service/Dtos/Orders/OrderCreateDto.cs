using OnlineMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Common;
using TwistFood.Domain.Entities.Order;
using TwistFood.Domain.Enums;
using TwistFood.Service.Attributes;

namespace TwistFood.Service.Dtos.Orders
{
    public class OrderCreateDto
    {
        [Required]
        public long UserId { get; set; }
        [Required]
        public Location Ilocation { get; set; }
        [Required, Integer]
        public double TotalSum { get; set; }
        public PaymentType paymentType { get; set; } = PaymentType.Cash;
        public OrderStatus orderStatus { get; set; } = OrderStatus.InQueue;
        [Required, Integer]
        public double DeliverPrice { get; set; }
        [Required]
        public bool IsDiscount { get; set; }    
        public long? DiscountId { get; set; }

        public static implicit operator Order (OrderCreateDto dto) 
        {
            return new Order()
            {
                UserId = dto.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt= DateTime.UtcNow, 
                DeleviryPrice = dto.DeliverPrice,
                IsDiscount = dto.IsDiscount,
                PaymentType = dto.paymentType,
                Status = dto.orderStatus,
                TotalSum = dto.TotalSum

            };
        }
    }
}
