using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IDiscountService
    {
        public Task<IEnumerable<Discount>> GetAllAsync(PagenationParams @params);
        public Task<Discount> GetAsync(int id);
        public Task<bool> CreateAsync(Discount discount);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(int id, Discount discount);
    }
}
