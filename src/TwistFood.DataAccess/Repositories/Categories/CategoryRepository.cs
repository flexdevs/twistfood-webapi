using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces;
using TwistFood.DataAccess.Interfaces.Categories;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Repositories.Categories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
