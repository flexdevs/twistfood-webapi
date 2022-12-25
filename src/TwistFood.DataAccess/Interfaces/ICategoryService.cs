using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface ICategoryService
    {
        public Task<IEnumerable<Category>> GetAllAsync(PagenationParams @params);
        public Task<Category> GetAsync(int id);
        public Task<bool> CreateAsync(Category category);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(int id, Category category);
    }
}
