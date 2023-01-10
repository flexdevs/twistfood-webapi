using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces.Employees;
using TwistFood.DataAccess.Interfaces.Orders;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Order;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Repositories.Orders
{
    public class OrderDetailRepository : GenericRepository<OrderDetail>, IOrderDetailRepository
    {
        public OrderDetailRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public IQueryable<OrderDetail> GetAll(long orderId)
        => _dbSet.Where(x => x.OrderId == orderId);
    }
}

