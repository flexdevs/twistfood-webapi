using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TwistFood.Service.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public  class PassportSeriaAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null) new ValidationResult("Pasport Seria can not be null!");
            Regex regex = new Regex(@"^(?!){0}([A-Z]{2} [0-9]{7})$");
            if (regex.Match(value.ToString()!).Success)
                return ValidationResult.Success;

            return new ValidationResult("Enter correct Passport Seria!");
        }
    }
}
