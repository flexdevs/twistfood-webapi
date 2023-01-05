using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;

namespace TwistFood.Service.Interfaces.Accounts
{
    public interface IAccountService
    {
        public Task<string> AccountLoginAsync(AccountLoginDto accountLoginDto);
        public Task<bool> AccountRegisterAsync(AccountRegisterDto accountRegisterDto);
    }
}
