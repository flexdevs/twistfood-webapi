using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Products;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos.Products;
using TwistFood.Service.Services.Categories;
using TwistFood.Service.ViewModels.Products;

namespace TwistFood.Service.Interfaces.Products
{
    public interface IProductService
    {
        public Task<bool> CreateProductAsync(CreateProductsDto createProductsDto);
        public Task<IEnumerable<ProductViewModel>> GetAllAsync(PagenationParams @params);
        public Task<IEnumerable<ProductViewModel>> SearchByNameAsync(string name);

        public Task<ProductViewModel> GetAsync(long id);

        public Task<bool> DeleteAsync(long id);

        public Task<bool> UpdateAsync(long id, UpdateProductDto updateProductDto);
        public Task<IEnumerable<Product>> GetAllForSearchAsync(string categoryName,string searchName); 
        
    }
}
