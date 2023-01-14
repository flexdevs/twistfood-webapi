
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Products;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos;
using TwistFood.Service.Interfaces.Categories;
using TwistFood.Service.Interfaces.Common;
using TwistFood.Service.Services.Common;
using TwistFood.Service.ViewModels.Categories;
using TwistFood.Service.ViewModels.Products;

namespace TwistFood.Service.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unitOfWork;
        private IPaginatorService _paginatorService;

        public CategoryService(IUnitOfWork unitOfWork, IPaginatorService paginatorService)
        {
            _unitOfWork = unitOfWork;
            _paginatorService = paginatorService;
        }
        public async Task<bool> CreateCategoryAsync(CategoryDto categoryDto)
        {
            var res = await _unitOfWork.Categories.FirstOrDefaultAsync(x => x.CategoryName == categoryDto.CategoryName);
            if (res is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Category name is already exist");

            Category category = (Category)categoryDto;

            _unitOfWork.Categories.Add(category);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(PagenationParams @params)
        {
            var query = _unitOfWork.Categories.GetAll()
            .OrderBy(x => x.Id);

            return await _paginatorService.ToPageAsync(query,
                @params.PageNumber, @params.PageSize);
        }

        public async Task<CategoryViewModels> GetAsync(long id)
        {
            var category = await _unitOfWork.Categories.FindByIdAsync(id);
            if (category is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");

            var products = _unitOfWork.Products.GetAll().AsNoTracking().Where(x => x.CategoryId == category.Id).ToList();

            List<ProductViewModel> list = new List<ProductViewModel>();

            foreach ( var product in products)
            {
                ProductViewModel productViewModel = new ProductViewModel()
                {
                    Id = product.Id,
                    ImagePath = product.ImagePath,
                    Price = product.Price,
                    ProductDescription = product.ProductDescription,
                    ProductName = product.ProductName
                };
                list.Add(productViewModel);

            }

            return new CategoryViewModels()
                    {
                        Id = category.Id,
                        CategoryName = category.CategoryName,
                        Products = list
                    };
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var res = await _unitOfWork.Categories.FindByIdAsync(id);
            if (res is not null)
            {
                _unitOfWork.Categories.Delete(res.Id);
                var result = await _unitOfWork.SaveChangesAsync();

                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");
        }
    }
}
