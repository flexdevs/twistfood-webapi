using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos.Products;
using TwistFood.Service.Services.Categories;

namespace TwistFood.Service.Interfaces.Products
{
    public interface IProductService
    {
        public Task<bool> CreateProductAsync(CreateProductsDto createProductsDto);
    }
}
