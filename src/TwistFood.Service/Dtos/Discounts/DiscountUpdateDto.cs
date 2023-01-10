using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Discounts;
using TwistFood.Service.Attributes;

namespace TwistFood.Service.Dtos.Discounts
{
    public class DiscountUpdateDto
    {
        public string? DiscountName { get; set; }
        public string? Description { get; set; } = string.Empty;
        [MaxFileSize(2), AllowedFiles(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]
        public IFormFile? Image { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        [Integer]
        public double? Price { get; set; }

        public static implicit operator Discount(DiscountUpdateDto dto)
        {
            return new Discount()
            {
                UpdatedAt = DateTime.UtcNow
            };

        }
    }
}
