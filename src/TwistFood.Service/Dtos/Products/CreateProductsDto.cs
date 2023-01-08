using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Products;
using TwistFood.Service.Attributes;

namespace TwistFood.Service.Dtos.Products
{
    public class CreateProductsDto
    {
        [Required]
        public long CategoryId { get; set; }
        
        [Required, MaxLength(60), MinLength(2)]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string ProductDescription { get; set; } = string.Empty;

        [Required, MaxFileSize(2), AllowedFiles(new string[] { ".jpg", ".png", ".jpeg", ".svg", ".webp" })]
        public IFormFile? Image { get; set; }
        [Required, Integer]
        public double Price { get; set; }

        public static implicit operator Product(CreateProductsDto dto)
        {
            return new Product()
            {
                ProductName = dto.ProductName,
                ProductDescription = dto.ProductDescription,
                Price = dto.Price,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}
