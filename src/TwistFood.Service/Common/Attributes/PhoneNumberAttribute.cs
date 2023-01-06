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
            Regex regex = new Regex("^(?:\\+998([- ])?(90|91|93|94|95|98|99|33|97|71|99|88)([- ])?(\\d{3})([- ])?(\\d{2})([- ])?(\\d{2}))");

            return regex.Match(phoneNumber).Success ? ValidationResult.Success
                : new ValidationResult("Please enter valid phone number. Phone must be contains only numbers or + character");
        }
    }
}
