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
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;

        public CategoryService(AppDbContext appDbContext, IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(Category category)
        {
            AppDbContex.Categories.Add(category);
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await AppDbContex.Categories.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found"); }
            AppDbContex.Categories.Remove(entity);
            var result = await AppDbContex.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Category>> GetAllAsync(PagenationParams @params)
        {
           var query = AppDbContex.Categories.OrderBy(x => x.Id).ThenBy(x => x.CategoryName).AsNoTracking();

            var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize);

            return data;
        }

        public async Task<Category> GetAsync(int id)
        {
            var result = await AppDbContex.Categories.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(int id, Category category)
        {
            var entity = await AppDbContex.Categories.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                category.Id = id;
                AppDbContex.Categories.Update(category);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Category not found");
        }
    }
}
