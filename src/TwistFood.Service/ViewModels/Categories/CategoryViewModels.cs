using OnlineMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.ViewModels.Products;

namespace TwistFood.Service.ViewModels.Categories
{
    public class CategoryViewModels :BaseEntity
    {
        public string CategoryName { get; set; } = string.Empty;
        public List<ProductViewModel> Products { get; set; }    

    }
}
