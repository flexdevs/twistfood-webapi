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
using TwistFood.DataAccess.Services.Common;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;

        public AdminService(AppDbContext appDbContext,IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(Admin admin)
        {
            AppDbContex.Admins.Add(admin);
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await AppDbContex.Admins.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Admin not found"); }
            AppDbContex.Admins.Remove(entity);
            var result = await AppDbContex.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Admin>> GetAllAsync(PagenationParams @params)
        {
            var query = AppDbContex.Admins.OrderBy(x => x.Id).ThenByDescending(x => x.FullName).AsNoTracking();

            var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize);

            return data;
        }

        public async Task<Admin> GetAsync(int id)
        {

            var result = await AppDbContex.Admins.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Admin not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(int id, Admin admin)
        {
            var entity = await AppDbContex.Admins.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                admin.Id = id;
                AppDbContex.Admins.Update(admin);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Admin not found");
        }
    }
}
