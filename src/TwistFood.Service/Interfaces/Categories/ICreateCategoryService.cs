using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos;

namespace TwistFood.Service.Interfaces.Categories
{
    public interface ICreateCategoryService
    {
        public Task<bool> CreateCategoryAsync(CategoryDto categoryDto);
    }
}
