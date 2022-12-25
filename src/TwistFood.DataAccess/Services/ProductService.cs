using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Interfaces;
using TwistFood.DataAccess.Common.Utils;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;

        public ProductService(AppDbContext appDbContext, IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(Product product)
        {
            AppDbContex.Products.Add(product);
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await AppDbContex.Products.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found"); }
            AppDbContex.Products.Remove(entity);
            var result = await AppDbContex.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Product>> GetAllAsync(PagenationParams @params)
        {
            var query = AppDbContex.Products.OrderBy(x => x.Id).ThenBy(x => x.ProductName).AsNoTracking();

            var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize);

            return data;
        }

        public async Task<Product> GetAsync(long id)
        {
            var result = await AppDbContex.Products.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(long id, Product product)
        {
            var entity = await AppDbContex.Products.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                product.Id = id;
                AppDbContex.Products.Update(product);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Product not found");
        }
    }
}
