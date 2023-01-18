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
using TwistFood.Service.ViewModels.Discounts;

namespace TwistFood.Service.Interfaces.Discounts
{
    public interface IDiscountService
    {
        public Task<bool> CreateDiscountAsync(DiscountCreateDto discountCreateDto);
        public Task<IEnumerable<DiscountViewModel>> GetAllAsync(PagenationParams @params);

        public Task<DiscountViewModel> GetAsync(long id);

        public Task<bool> DeleteAsync(long id);

        public Task<bool> UpdateAsync(long id, DiscountUpdateDto discountUpdateDto);
    }
}
