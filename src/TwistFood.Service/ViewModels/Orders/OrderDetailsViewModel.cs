using OnlineMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistFood.Service.ViewModels.Orders
{
    public class OrderDetailViewModel : BaseEntity
    {
        public string ProductName { get; set; } = string.Empty;
        public double Price { get; set; }

    }
}
