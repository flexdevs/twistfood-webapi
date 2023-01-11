
using System;
using System.Collections.Generic;
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

        public async Task<Category> GetAsync(long id)
        {
            var res = await _unitOfWork.Categories.FindByIdAsync(id);
            if (res is not null)
            {
                return res;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");
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
