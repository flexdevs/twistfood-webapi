using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Users;
using TwistFood.Service.Common.Attributes;

namespace TwistFood.Service.Dtos.Account
{
    public class AccountRegisterDto
    {
        public string? TelegramId { get; set; }
        [Required, MaxLength(60), MinLength(2)]
        public string FullName { get; set; }    
        [Required,PhoneNumber]
        public string PhoneNumber { get; set; } = string.Empty;
        public string? PhoneId { get; set; }

        public static implicit operator User(AccountRegisterDto accountRegisterDto)
        {
            return new User()
            {
                FullName = accountRegisterDto.FullName,
                PhoneNumber = accountRegisterDto.PhoneNumber,
                TelegramId = accountRegisterDto.TelegramId
            };
        }  
    }
   
}
