using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Products;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos;
using TwistFood.Service.ViewModels.Categories;

namespace TwistFood.Service.Interfaces.Categories
{
    public interface ICategoryService
    {
        public Task<bool> CreateCategoryAsync(CategoryDto categoryDto);

        public Task<IEnumerable<Category>> GetAllAsync(PagenationParams @params);

        public Task<CategoryViewModels> GetAsync(long id);

        public Task<bool> DeleteAsync(long id);
    }
}
