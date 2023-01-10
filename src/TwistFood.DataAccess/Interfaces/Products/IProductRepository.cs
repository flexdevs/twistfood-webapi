using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Products;

namespace TwistFood.DataAccess.Interfaces.Products
{
    public interface IProductRepository : IGenericRepository<Product>
    {
    }
}
