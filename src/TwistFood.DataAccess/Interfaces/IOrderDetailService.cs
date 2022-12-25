using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IOrderDetailService
    {
        public Task<IEnumerable<OrderDetail>> GetAllAsync(PagenationParams @params);
        public Task<OrderDetail> GetAsync(long id);
        public Task<bool> CreateAsync(OrderDetail orderDetail);
        public Task<bool> DeleteAsync(long id);
        public Task<bool> UpdateAsync(long id, OrderDetail orderDetail);
    }
}
