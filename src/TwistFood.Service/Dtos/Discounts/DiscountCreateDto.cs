using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Discounts;
using TwistFood.Service.Attributes;

namespace TwistFood.Service.Dtos
{
    public class DiscountCreateDto
    {
        [Required,MaxLength(60),MinLength(2)]
        public string DiscountName { get; set; } = string.Empty;
        [Required,MaxLength(300),MinLength(2)]
        public string Description { get; set; } = string.Empty;
        [Required,MaxFileSize(2), AllowedFiles(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]
        public IFormFile? Image { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        [Required,Integer]
        public double Price { get; set; }

        public static implicit operator Discount(DiscountCreateDto discountDto)
        {
            return new Discount()
            {
                DiscountName = discountDto.DiscountName,
                Description = discountDto.Description,
                StartTime = discountDto.StartTime.ToUniversalTime(),
                EndTime = discountDto.EndTime.ToUniversalTime() ,
                Price = discountDto.Price,
                CreatedAt = DateTime.UtcNow
            };
            
        }

    }
}
