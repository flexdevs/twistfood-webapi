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

namespace TwistFood.Service.Dtos
{
    public class DeliverRegistrDto
    {
        [Required,MaxLength(60),MinLength(2)]
        public string FirstName { get; set; }    = string.Empty;
        [Required, MaxLength(60), MinLength(2)]
        public string LastName { get; set; } = string.Empty;

        [Required,PhoneNumber]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public DateTime BirthDate { get; set; }
        [Required, MaxFileSize(2), AllowedFiles(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]
        public IFormFile? Image { get; set; }
        [Required, Integer]
        public double Salary { get; set; }
        [Required,PassportSeria]
        public string PassportSeriaNumber { get; set; } = string.Empty; 


        public static implicit operator Deliver(DeliverRegistrDto dto)
        {
            return new Deliver()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,    
                PhoneNumber = dto.PhoneNumber,
                BirthDate = dto.BirthDate.ToUniversalTime(),
                Salary = dto.Salary,
                PassportSeriaNumber = dto.PassportSeriaNumber,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt= DateTime.UtcNow
            };
        }
    }
    
}
