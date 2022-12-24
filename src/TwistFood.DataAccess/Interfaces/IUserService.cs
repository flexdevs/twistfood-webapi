using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;
namespace TwistFood.DataAccess.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllAsync(PagenationParams @params);
        public Task<User> GetAsync(long id);
        public Task<bool> CreateAsync(User user);   
        public Task<bool> DeleteAsync(long id);
        public Task<bool> UpdateAsync(long id,User user);
    }
}
