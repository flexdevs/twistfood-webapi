
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Entities.Order;
using TwistFood.Domain.Entities.Phones;
using TwistFood.Domain.Entities.Users;
using TwistFood.Domain.Exceptions;
using TwistFood.Service.Common.Helpers;
using TwistFood.Service.Common.Utils;
using TwistFood.Service.Dtos;
using TwistFood.Service.Dtos.Account;
using TwistFood.Service.Dtos.Accounts;
using TwistFood.Service.Interfaces;
using TwistFood.Service.Interfaces.Accounts;
using TwistFood.Service.Interfaces.Common;
using TwistFood.Service.Services.Common;

namespace TwistFood.Service.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private IUnitOfWork _unitOfWork;
        private IAuthManager _authManager;
        private IPaginatorService _paginatorService;

        public AccountService(IUnitOfWork unitOfWork, 
                              IAuthManager authManager,
                              IPaginatorService paginatorService)
        {
            _unitOfWork = unitOfWork;
            _authManager = authManager;
            _paginatorService = paginatorService;
        }

     
        public async Task<bool> AccountRegisterAsync(AccountRegisterDto accountRegisterDto)
        {
            var res = await _unitOfWork.Users.FirstOrDefaultAsync(x => x.PhoneNumber == accountRegisterDto.PhoneNumber);
            if (res is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "User is already exist");
            
            User user = new User() 
            {
                CreatedAt= DateTime.UtcNow, 
                UpdatedAt= DateTime.UtcNow, 
                FullName = accountRegisterDto.FullName,  
                PhoneNumber= accountRegisterDto.PhoneNumber  
                  
            };
            if (accountRegisterDto.TelegramId!= null) 
            {
                user.TelegramId = accountRegisterDto.TelegramId;
            }
            _unitOfWork.Users.Add(user);    
            await _unitOfWork.SaveChangesAsync();   


            if (accountRegisterDto.PhoneId!= null) 
            {
                var user1 = await _unitOfWork.Users.FirstOrDefaultAsync(x => x.PhoneNumber == accountRegisterDto.PhoneNumber);

                Phone phone = new Phone()
                {
                    PhoneId = accountRegisterDto.PhoneId,
                    UpdatedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    UserId = user1!.Id,
                    Status = "Active"
                };
                _unitOfWork.Phones.Add(phone);  
               await  _unitOfWork.SaveChangesAsync();
            }

            return true;

        }

        public async Task<bool> AccountUpdateAsync(AccountUpdateDto accountUpdateDto)
        {
            var user = await _unitOfWork.Users.FindByIdAsync(HttpContextHelper.UserId);
            if (user == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!"); }

            if(accountUpdateDto.FullName is not null)
            {
                user.FullName = accountUpdateDto.FullName;
            }

            _unitOfWork.Users.Update(HttpContextHelper.UserId, user);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<User>> GetAllAsync(PagenationParams @params)
        {
            var query = _unitOfWork.Users.GetAll()
            .OrderBy(x => x.Id);

            return await _paginatorService.ToPageAsync(query,
                @params.PageNumber, @params.PageSize);
        }

        public async Task<User> GetAsync(long id)
        {
            var res = await _unitOfWork.Users.FindByIdAsync(id);
            if (res is not null)
            {
                return res;
            }
            else throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");
        }
    }
}
