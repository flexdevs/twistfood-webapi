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
    public class PhoneService : IPhoneService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;

        public PhoneService(AppDbContext appDbContext, IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(Phone phone)
        {
            AppDbContex.Phones.Add(phone);
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await AppDbContex.Phones.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Phone not found"); }
            AppDbContex.Phones.Remove(entity);
            var result = await AppDbContex.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Phone>> GetAllAsync(PagenationParams @params)
        {
            var query = AppDbContex.Phones.OrderBy(x => x.Id).ThenBy(x => x.PhoneId).AsNoTracking();

            var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize);

            return data;
        }

        public async Task<Phone> GetAsync(long id)
        {
            var result = await AppDbContex.Phones.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Phone not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(long id, Phone phone)
        {
            var entity = await AppDbContex.Phones.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                phone.Id = id;
                AppDbContex.Phones.Update(phone);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Phone not found");
        }
    }
}
