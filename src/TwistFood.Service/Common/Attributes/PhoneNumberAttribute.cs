using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwistFood.Service.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            string phoneNumber = (string)value!;
            Regex regex = new Regex("^(?:\\+\\([9]{2}[8]\\)[0-9]{2}\\ [0-9]{3}\\-[0-9]{2}\\-[0-9]{2})");

            return regex.Match(phoneNumber).Success ? ValidationResult.Success
                : new ValidationResult("Please enter valid phone number. Phone must be contains only numbers or + character");
        }
    }
}
