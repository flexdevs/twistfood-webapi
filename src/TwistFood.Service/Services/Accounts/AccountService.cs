using CarShop.Api.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces;
using TwistFood.Domain.Entities.Phones;
using TwistFood.Domain.Entities.Users;
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
            if (user == null) { throw new StatusCodeException(HttpStatusCode.NotFound, "User not found, Phone Number is incorrect!"); }
            if (accountLoginDto.TelegramId != null)
            {
                if (user.TelegramId != null && user.TelegramId != accountLoginDto.TelegramId)
                {
                    user.TelegramId = accountLoginDto.TelegramId;
                }
            }
            if (accountLoginDto.PhoneId!= null) 
            {
                var phone = await _unitOfWork.Phones.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (phone == null || phone.PhoneId != accountLoginDto.PhoneId) 
                {
                    Phone newPhone = new Phone()
                    {
                        PhoneId= accountLoginDto.PhoneId,   
                        UserId= user.Id,
                        CreatedAt= DateTime.UtcNow,
                        UpdatedAt= DateTime.UtcNow,
                        Status = "Active",
                                             
                    };
                    _unitOfWork.Phones.Add(newPhone);

                }
                
            }
            await _unitOfWork.SaveChangesAsync();

            return _authManager.GenerateToken(user);
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
    }
}
