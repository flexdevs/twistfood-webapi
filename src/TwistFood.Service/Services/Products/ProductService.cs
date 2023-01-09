using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Products;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Products;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Products;

namespace TwistFood.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;

        public ProductService(IUnitOfWork unitOfWork, IFileService fileService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
        }
        public async Task<bool> CreateProductAsync(CreateProductsDto createProductsDto)
        {
            var res = await _unitOfWork.Products.FirstOrDefaultAsync(x => x.ProductName == createProductsDto.ProductName);
            if (res is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "Product is already exist");

            Product product = (Product)createProductsDto;

            if (createProductsDto.Image is not null)
            {
                product.ImagePath = await _fileService.SaveImageAsync(createProductsDto.Image);
            }
            /*var category = await _unitOfWork.Categories.FindByIdAsync(product.CategoryId);
            if(category is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");
            }*/
            product.CategoryId = createProductsDto.CategoryId;
            _unitOfWork.Products.Add(product);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var res = await _unitOfWork.Products.FindByIdAsync(id);
            if (res is not null)
            {
                _unitOfWork.Products.Delete(id);
                var result = await _unitOfWork.SaveChangesAsync();

                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var query = _unitOfWork.Products.GetAll();

            return await query.OrderBy(x => x.Id).ThenByDescending(x => x.Price)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Product> GetAsync(long id)
        {
            var res = await _unitOfWork.Products.FindByIdAsync(id);
            if (res is not null)
            {
                return res;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");
        }

        public async Task<bool> UpdateAsync(long id, Product obj)
        {
            var res = await _unitOfWork.Products.FindByIdAsync(id);
            if (res is not null)
            {
                _unitOfWork.Entry(res).State = EntityState.Detached;
                obj.Id = id;
                _unitOfWork.Products.Update(id, obj);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");
        }
    }
}
