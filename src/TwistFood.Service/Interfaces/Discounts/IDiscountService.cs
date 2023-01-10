using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Discounts;
using TwistFood.Domain.Entities.Products;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Discounts;
using TwistFood.Service.Common.Utils;

namespace TwistFood.Service.Interfaces.Discounts
{
    public interface IDiscountService
    {
        public Task<bool> CreateDiscountAsync(DiscountCreateDto discountCreateDto);
        public Task<IEnumerable<Discount>> GetAllAsync(PagenationParams @params);

        public Task<Discount> GetAsync(long id);

        public Task<bool> DeleteAsync(long id);

        public Task<bool> UpdateAsync(long id, DiscountUpdateDto discountUpdateDto);
    }
}
