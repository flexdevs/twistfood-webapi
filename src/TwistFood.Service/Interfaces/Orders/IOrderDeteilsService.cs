using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos.Orders;

namespace TwistFood.Service.Interfaces.Orders
{
    public interface IOrderDeteilsService
    {
        public Task<bool> OrderCreateAsync(OrderCreateDto orderCreateDto,List<OrderDeteilsCreateDto> orderDeteilsDto);
        public Task<bool> OrderUpdateAsync(long orderId,List<OrderDetailUpdateDto> dto);
    }
}
