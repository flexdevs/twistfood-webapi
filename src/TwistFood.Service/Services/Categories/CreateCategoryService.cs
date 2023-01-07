using CarShop.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Dtos;
using TwistFood.Service.Interfaces.Categories;

namespace TwistFood.Service.Services.Categories
{
    public class CreateCategoryService : ICreateCategoryService
    {
        private IUnitOfWork _unitOfWork;

        public CreateCategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
    }
}
