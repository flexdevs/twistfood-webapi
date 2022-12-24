using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.Models;
using TwistFood.DataAccess.Common.Utils;

namespace TwistFood.DataAccess.Interfaces;

public interface IAdminService
{
    public Task<IEnumerable<Admin>> GetAllAsync(PagenationParams @params);
    public Task<Admin> GetAsync(int id);
    public Task<bool> CreateAsync(Admin admin);
    public Task<bool> DeleteAsync(int id);
    public Task<bool> UpdateAsync(int id, Admin admin);
}
