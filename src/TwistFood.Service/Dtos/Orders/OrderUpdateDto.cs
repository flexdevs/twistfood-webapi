using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Order;

namespace TwistFood.Service.Dtos.Orders
{
    public class OrderUpdateDto 
    {
        public long OrderId { get; set; }
        public long? DeliverId { get; set; }   
        public long? OperatorId { get; set; }    
        public double? TotalSum { get; set; }  
        public DateTime? DeliveredAt { get; set; }

        
    }
}
