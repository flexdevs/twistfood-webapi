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
    public class OrderDetailService : IOrderDetailService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;

        public OrderDetailService(AppDbContext appDbContext, IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(OrderDetail orderDetail)
        {
            AppDbContex.OrderDetails.Add(orderDetail);
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await AppDbContex.OrderDetails.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "OrderDetail not found"); }
            AppDbContex.OrderDetails.Remove(entity);
            var result = await AppDbContex.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync(PagenationParams @params)
        {
            var query = AppDbContex.OrderDetails.OrderBy(x => x.Id).ThenBy(x => x.Price).AsNoTracking();

            var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize);

            return data;
        }

        public async Task<OrderDetail> GetAsync(long id)
        {
            var result = await AppDbContex.OrderDetails.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "OrderDetail not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(long id, OrderDetail orderDetail)
        {
            var entity = await AppDbContex.OrderDetails.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                orderDetail.Id = id;
                AppDbContex.OrderDetails.Update(orderDetail);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "OrderDetail not found");
        }
    }
}
