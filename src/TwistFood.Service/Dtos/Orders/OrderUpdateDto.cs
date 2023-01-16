using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Order;
using TwistFood.Domain.Enums;

namespace TwistFood.Service.Dtos.Orders
{
    public class OrderUpdateDto 
    {
        [Required]
        public long OrderId { get; set; }
        public long? DeliverId { get; set; }   
        public long? OperatorId { get; set; }    
        public double? TotalSum { get; set; }  
        public DateTime? DeliveredAt { get; set; }

        public OrderStatus? Status { get; set; }
    }
}
