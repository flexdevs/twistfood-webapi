using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Order;
using TwistFood.Service.Dtos.Orders;

namespace TwistFood.Service.Interfaces.Orders
{
    public interface IOrderService
    {
        public Task<Order> OrderCreateAsync(OrderCreateDto dto);
        public Task<bool> OrderUpdateAsync(OrderUpdateDto dto);
    }
}
