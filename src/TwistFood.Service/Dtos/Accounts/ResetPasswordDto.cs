using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwistFood.Service.Attributes;

namespace TwistFood.Service.Dtos.Accounts
{
    public class ResetPasswordDto
    {
        [Required, Email]
        public string Email { get; set; } = string.Empty;

        [Required]
        public uint Code { get; set; }

        [Required(ErrorMessage = "Password is required"), MinLength(8), MaxLength(50)]
        public string Password { get; set; } = string.Empty;
    }
}
