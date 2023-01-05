using CarShop.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Interfaces.Accounts;

namespace TwistFood.Service.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private IAuthManager _authManager;

        public AccountService(IUnitOfWork unitOfWork, IAuthManager authManager)
        {
            _unitOfWork = unitOfWork;
            _authManager = authManager; 
        }

        public async Task<string> AccountLoginAsync(AccountLoginDto accountLoginDto)
        {
            var user = await _unitOfWork.Users.FirstOrDefaultAsync(x => x.PhoneNumber == accountLoginDto.PhoneNumber);
            if (user == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "User not found, Email is incorrect!"); }
            return _authManager.GenerateToken(user);
        }

        public Task<bool> AccountRegistrAsync(AccountRegistrDto accountRegistrDto)
        {
            throw new NotImplementedException();
        }
    }
}
