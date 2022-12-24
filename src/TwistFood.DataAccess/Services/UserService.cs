using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Interfaces;
using TwistFood.DataAccess.Common.Utils;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext AppDbContex;
        private readonly IPaginatorService _paginator;

        public UserService(AppDbContext appDbContext, IPaginatorService paginatorService)
        {
            this.AppDbContex = appDbContext;  
            this._paginator = paginatorService;
        }
        public async Task<bool> CreateAsync(User user)
        {
            AppDbContex.Users.Add(user);    
            var result = await AppDbContex.SaveChangesAsync();

            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
           var entity = await  AppDbContex.Users.FindAsync(id);
            if (entity == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "User not found"); }
            AppDbContex.Users.Remove(entity);   
            var result = await AppDbContex.SaveChangesAsync();
            return result>0;    
        }

        public async Task<IEnumerable<User>> GetAllAsync( PagenationParams @params)
        {
            var query = AppDbContex.Users.OrderBy(x => x.Id).ThenByDescending(x => x.FullName).AsNoTracking();

           var data = await _paginator.ToPagedAsync(query, @params.PageNumber, @params.PageSize); 
            
            return data;
        }

        public async Task<User> GetAsync(long id)
        {
            var result = await AppDbContex.Users.FindAsync(id);
            if (result is null) throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");
            else return result;
        }

        public async Task<bool> UpdateAsync(long id, User user)
        {
            var entity = await AppDbContex.Users.FindAsync(id);
            if (entity is not null)
            {
                AppDbContex.Entry(entity!).State = EntityState.Detached;
                user.Id = id;
                AppDbContex.Users.Update(user);
                var result = await AppDbContex.SaveChangesAsync();
                return result > 0;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");
        }
    }
}
