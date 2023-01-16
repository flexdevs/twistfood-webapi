using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Builders;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Order;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos.Orders;
using TwistFood.Service.ViewModels.Orders;

namespace TwistFood.Service.Interfaces.Orders
{
    public interface IOrderService
    {
        public Task<long> OrderCreateAsync(OrderCreateDto dto);
        public Task<bool> OrderUpdateAsync(OrderUpdateDto dto);
        public Task<IEnumerable<OrderViewModel>> GetAllAsync(PagenationParams @params);
        public Task<OrderWithOrderDetailsViewModel> GetOrderWithOrderDetailsAsync(long OrderId);
    }
}
