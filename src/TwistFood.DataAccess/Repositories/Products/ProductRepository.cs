using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces.Categories;
using TwistFood.DataAccess.Interfaces.Products;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Products;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Repositories.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
