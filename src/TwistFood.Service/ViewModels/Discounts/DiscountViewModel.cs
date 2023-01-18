using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistFood.Service.ViewModels.Discounts
{
    public class DiscountViewModel
    {
        public long Id { get; set; }
        public string DiscountName { get; set; } = string.Empty;
        public string DiscountDescription { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = String.Empty;
        public double Price { get; set; }
        public string ImagePath { get; set; } = string.Empty;
    }
}
