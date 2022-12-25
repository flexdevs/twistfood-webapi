using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IPhoneService
    {
        public Task<IEnumerable<Phone>> GetAllAsync(PagenationParams @params);
        public Task<Phone> GetAsync(long id);
        public Task<bool> CreateAsync(Phone phone);
        public Task<bool> DeleteAsync(long id);
        public Task<bool> UpdateAsync(long id, Phone phone);
    }
}
