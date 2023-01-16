using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Products;
using TwistFood.Domain.Entities.Users;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Dtos.Accounts;

namespace TwistFood.Service.Interfaces.Accounts
{
    public interface IAccountService
    {
        public Task<string> AccountLoginAsync(AccountLoginDto accountLoginDto);
        public Task<bool> AccountRegisterAsync(AccountRegisterDto accountRegisterDto);
        public Task<bool> AccountUpdateAsync(AccountUpdateDto accountUpdateDto);

        public Task<IEnumerable<User>> GetAllAsync(PagenationParams @params);

        public Task<User> GetAsync(long id);
    }
}
