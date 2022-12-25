using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface ILocationService
    {
        public Task<IEnumerable<Location>> GetAllAsync(PagenationParams @params);
        public Task<Location> GetAsync(long id);
        public Task<bool> CreateAsync(Location location);
        public Task<bool> DeleteAsync(long id);
        public Task<bool> UpdateAsync(long id, Location location);
    }
}
