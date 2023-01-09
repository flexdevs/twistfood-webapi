using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Products;
using TwistFood.Service.Attributes;

namespace TwistFood.Service.Dtos.Products
{
    public class UpdateProductDto
    {
        public long? CategoryId { get; set; }
        public string? ProductName { get; set; }

        public string? ProductDescription { get; set; }

        [MaxFileSize(2), AllowedFiles(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]
        public IFormFile? Image { get; set; }
        [Integer]
        public double? Price { get; set; }

        public static implicit operator Product(UpdateProductDto dto)
        {
            return new Product()
            {
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
