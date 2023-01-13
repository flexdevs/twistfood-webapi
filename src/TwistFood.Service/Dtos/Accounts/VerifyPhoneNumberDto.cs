using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Common.Attributes;

namespace TwistFood.Service.Dtos.Accounts
{
    public class VerifyPhoneNumberDto
    {
        [Required, PhoneNumber]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public int Code { get; set; }
    }
}
