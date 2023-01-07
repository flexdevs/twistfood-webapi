using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Common.Attributes;

namespace TwistFood.Service.Dtos.Operators
{
    public class OperatorRegisterDto
    {
        [Required, MaxLength(60), MinLength(2)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(60), MinLength(2)]
        public string LastName { get; set; } = string.Empty;

        [Required, PhoneNumber]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
