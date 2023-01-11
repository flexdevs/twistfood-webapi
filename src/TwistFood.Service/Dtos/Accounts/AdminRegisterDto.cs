using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Service.Attributes;
using TwistFood.Service.Common.Attributes;
using TwistFood.Service.Dtos.Operators;

namespace TwistFood.Service.Dtos.Accounts
{
    public class AdminRegisterDto
    {
        [Required, MaxLength(60), MinLength(2)]
        public string FirstName { get; set; } = string.Empty;
        [Required, MaxLength(60), MinLength(2)]
        public string LastName { get; set; } = string.Empty;

        [Required, PhoneNumber]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public DateTime BirthDate { get; set; }
        [Required, MaxFileSize(2), AllowedFiles(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]
        public IFormFile? Image { get; set; }
        [Required, Integer]
        public double Salary { get; set; }
        [Required, PassportSeria]
        public string PassportSeriaNumber { get; set; } = string.Empty;

        [Required, Email]
        public string Email { get; set; } = string.Empty;

        [Required, StrongPassword]
        public string Password { get; set; } = string.Empty;

        [Required]
        public bool IsHead { get; set; }

        public static implicit operator Admin(AdminRegisterDto dto)
        {
            return new Admin()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                BirthDate = dto.BirthDate.ToUniversalTime(),
                Salary = dto.Salary,
                PassportSeriaNumber = dto.PassportSeriaNumber,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsHead= dto.IsHead,
            };
        }
    }
}
