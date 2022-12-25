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
    public class OperatorService : IOperatorService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;
        public OperatorService(AppDbContext appDbContext, IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(Operator obj)
        {
            AppDbContex.Operators.Add(obj);
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }


        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await AppDbContex.Operators.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Operator not found"); }
            AppDbContex.Operators.Remove(entity);
            var result = await AppDbContex.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Operator>> GetAllAsync(PagenationParams @params)
        {
            var query = AppDbContex.Operators.OrderBy(x => x.Id).ThenBy(x => x.FullName).AsNoTracking();

            var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize);

            return data;
        }

        public async Task<Operator> GetAsync(int id)
        {
            var result = await AppDbContex.Operators.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Operator not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(int id, Operator obj)
        {
            var entity = await AppDbContex.Operators.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                obj.Id = id;
                AppDbContex.Operators.Update(obj);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Operator not found");
        }
    }
}
