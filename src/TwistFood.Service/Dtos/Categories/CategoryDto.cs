using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Categories;

namespace TwistFood.Service.Dtos
{
    public class CategoryDto
    {
        [Required,MaxLength(50),MinLength(2)]
        public string CategoryName { get; set; }   = string.Empty;

        public static implicit operator Category(CategoryDto category)
        {
            return new Category()
            {
                CategoryName = category.CategoryName,

            };
        }
    }
}
