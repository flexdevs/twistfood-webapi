using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IOperatorService
    {
        public Task<IEnumerable<Operator>> GetAllAsync(PagenationParams @params);
        public Task<Operator> GetAsync(int id);
        public Task<bool> CreateAsync(Operator obj);
        public Task<bool> DeleteAsync(int id);
        public Task<bool> UpdateAsync(int id, Operator obj);
    }
}
