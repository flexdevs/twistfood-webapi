using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces.Categories;
using TwistFood.DataAccess.Interfaces.Discounts;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Discounts;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Repositories.Discounts
{
    public class DiscountRepository : GenericRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
