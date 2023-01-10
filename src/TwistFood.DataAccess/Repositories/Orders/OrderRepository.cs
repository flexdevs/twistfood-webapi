using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces.Categories;
using TwistFood.DataAccess.Interfaces.Orders;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Order;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Repositories.Orders
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
