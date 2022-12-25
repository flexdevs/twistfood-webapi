using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IDeliverService
    {
        public Task<IEnumerable<Deliver>> GetAllAsync(PagenationParams @params);
        public Task<Deliver> GetAsync(int id);
        public Task<bool> CreateAsync(Deliver deliver);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(int id, Deliver deliver);
    }
}
