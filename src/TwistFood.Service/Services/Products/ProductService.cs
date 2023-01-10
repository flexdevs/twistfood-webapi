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
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Products;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Common;
using TwistFood.Service.Interfaces.Products;
using TwistFood.Service.Services.Common;

namespace TwistFood.Service.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        private readonly IPaginatorService _paginatorService;

        public ProductService(IUnitOfWork unitOfWork, 
                              IFileService fileService,
                              IPaginatorService paginatorService)
        {
            _unitOfWork = unitOfWork;
            _fileService = fileService;
            _paginatorService = paginatorService;
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
            var category = await _unitOfWork.Categories.FindByIdAsync(createProductsDto.CategoryId);
            if (category is null)
            {
                throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");
            }
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

        public async Task<IEnumerable<Product>> GetAllAsync(PagenationParams @params)
        {
            var query = _unitOfWork.Products.GetAll()
            .OrderBy(x => x.Id).ThenByDescending(x => x.Price);

            return await _paginatorService.ToPageAsync(query,
                @params.PageNumber, @params.PageSize);
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

        public async Task<bool> UpdateAsync(long id, UpdateProductDto updateProductDto)
        {
            var res = await _unitOfWork.Products.FindByIdAsync(id);
            if (res is not null)
            {
                _unitOfWork.Entry(res).State = EntityState.Detached;
                Product product = (Product)updateProductDto;

                product.Id = id;
                product.CreatedAt = res.CreatedAt;

                if (updateProductDto.CategoryId is not null)
                {
                    var category = await _unitOfWork.Categories.FindByIdAsync((long)updateProductDto.CategoryId);
                    if (category is null)
                    {
                        throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");
                    }
                    product.CategoryId = (long)updateProductDto.CategoryId;
                }
                else
                {
                    product.CategoryId = res.CategoryId;
                }

                if (updateProductDto.ProductName is not null)
                {
                    product.ProductName = updateProductDto.ProductName;
                }
                else
                {
                    product.ProductName = res.ProductName;
                }

                if (updateProductDto.ProductDescription is not null)
                {
                    product.ProductDescription = updateProductDto.ProductDescription;
                }
                else
                {
                    product.ProductDescription = res.ProductDescription;
                }

                if (updateProductDto.ProductDescription is not null)
                {
                    product.ProductDescription = updateProductDto.ProductDescription;
                }
                else
                {
                    product.ProductDescription = res.ProductDescription;
                }

                if (updateProductDto.Price is not null)
                {
                    product.Price = (long)updateProductDto.Price;
                }
                else
                {
                    product.Price = res.Price;
                }

                if (updateProductDto.Image is not null)
                {
                    product.ImagePath = await _fileService.SaveImageAsync(updateProductDto.Image);
                }
                else
                {
                    product.ImagePath = res.ImagePath;
                }
                
                _unitOfWork.Products.Update(id, product);
                var result = await _unitOfWork.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");
        }
    }
}
