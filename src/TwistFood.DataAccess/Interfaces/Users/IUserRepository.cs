using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Users;

namespace TwistFood.DataAccess.Interfaces.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
