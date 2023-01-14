using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Common.Attributes;

namespace TwistFood.Service.Dtos
{
    public class AccountLoginDto
    {
        /*public string? TelegramId { get; set; }*/
        [Required,PhoneNumber]
        public string PhoneNumber { get; set; } = string.Empty;
        /*public string? PhoneId { get; set; }*/
    }
}
