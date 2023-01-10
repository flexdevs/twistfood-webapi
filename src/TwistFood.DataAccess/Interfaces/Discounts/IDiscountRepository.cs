using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Discounts;

namespace TwistFood.DataAccess.Interfaces.Discounts
{
    public interface IDiscountRepository: IGenericRepository<Discount>
    {
    }
}
