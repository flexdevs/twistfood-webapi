using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.AccountAdmin;
using TwistFood.Service.Dtos.Accounts;

namespace TwistFood.Service.Interfaces.Admins
{
    public interface IAdminService
    {
        public Task<string> AdminLoginAsync(AdminLoginDto adminLoginDto);
        public Task<bool> AdminRegisterAsync(AdminRegisterDto adminRegisterDto);
    }
}
