using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces.Categories;
using TwistFood.DataAccess.Interfaces.Employees;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Repositories.Employees
{
    public class DeliverRepository : GenericRepository<Deliver>, IDeliverRepository
    {
        public DeliverRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
