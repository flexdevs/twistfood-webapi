using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<Product>> GetAllAsync(PagenationParams @params);
        public Task<Product> GetAsync(long id);
        public Task<bool> CreateAsync(Product product);
        public Task<bool> DeleteAsync(long id);
        public Task<bool> UpdateAsync(long id, Product product);
    }
}
