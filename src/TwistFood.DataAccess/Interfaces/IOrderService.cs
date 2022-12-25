using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IOrderService
    {
        public Task<IEnumerable<Order>> GetAllAsync(PagenationParams @params);
        public Task<Order> GetAsync(long id);
        public Task<bool> CreateAsync(Order order);
        public Task<bool> DeleteAsync(long id);
        public Task<bool> UpdateAsync(long id, Order order);
    }
}
