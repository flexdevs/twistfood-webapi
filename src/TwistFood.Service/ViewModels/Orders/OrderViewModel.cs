using Microsoft.AspNetCore.Identity;
using OnlineMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Common;
using TwistFood.Domain.Entities.Discounts;
using TwistFood.Domain.Enums;

namespace TwistFood.Service.ViewModels.Orders
{
    public class OrderViewModel : Auditable
    {
        public string UserPhoneNumber { get; set; } = string.Empty;
        public double TotalSum { get; set; }
        public string paymentType { get; set; } = string.Empty; 
        public string Status { get; set; } = string.Empty;
        public string OrderDetails { get; set; } = string.Empty;
        public long @operatorId { get; set; }

        public long deliverId { get; set; }

    }
}
