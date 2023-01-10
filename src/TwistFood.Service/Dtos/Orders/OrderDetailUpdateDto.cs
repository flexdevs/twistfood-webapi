using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistFood.Service.Dtos.Orders
{
    public class OrderDetailUpdateDto
    {
        public long? OrderDetailId { get; set; } 
        public long? ProductId { get; set; } 
        public int? Amount { get; set; } 
    }
}
