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
    public class DiscountService : IDiscountService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;

        public DiscountService(AppDbContext appDbContext, IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(Discount discount)
        {
            AppDbContex.Discounts.Add(discount);
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await AppDbContex.Discounts.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Discount not found"); }
            AppDbContex.Discounts.Remove(entity);
            var result = await AppDbContex.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Discount>> GetAllAsync(PagenationParams @params)
        {
            var query = AppDbContex.Discounts.OrderBy(x => x.Id).ThenBy(x => x.DiscountName).AsNoTracking();

            var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize);

            return data;
        }

        public async Task<Discount> GetAsync(int id)
        {
            var result = await AppDbContex.Discounts.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Discount not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(int id, Discount discount)
        {
            var entity = await AppDbContex.Discounts.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                discount.Id = id;
                AppDbContex.Discounts.Update(discount);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Discount not found");
        }
    }
}
