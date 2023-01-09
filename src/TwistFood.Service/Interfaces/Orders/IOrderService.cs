using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos.Orders;

namespace TwistFood.Service.Interfaces.Orders
{
    public interface IOrderService
    {
        public Task<bool> OrderCreateAsync(OrderCreateDto dto);
        public Task<bool> OrderUpdateAsync(OrderUpdateDto dto);
    }
}
