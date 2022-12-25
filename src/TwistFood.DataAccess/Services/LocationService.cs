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
    public class LocationService : ILocationService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;

        public LocationService(AppDbContext appDbContext, IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(Location location)
        {
            AppDbContex.Locations.Add(location);
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await AppDbContex.Locations.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "Location not found"); }
            AppDbContex.Locations.Remove(entity);
            var result = await AppDbContex.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<Location>> GetAllAsync(PagenationParams @params)
        {
            var query = AppDbContex.Locations.OrderBy(x => x.Id).ThenBy(x => x.AdditionInfo).AsNoTracking();

            var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize);

            return data;
        }

        public async Task<Location> GetAsync(long id)
        {
            var result = await AppDbContex.Locations.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "Location not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(long id, Location location)
        {
            var entity = await AppDbContex.Locations.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                location.Id = id;
                AppDbContex.Locations.Update(location);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "Location not found");
        }
    }
}
