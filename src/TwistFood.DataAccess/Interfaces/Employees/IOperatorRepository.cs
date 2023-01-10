using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Order;

namespace TwistFood.DataAccess.Interfaces.Employees
{
    public interface IOperatorRepository : IGenericRepository<Operator>
    {
    }
}
