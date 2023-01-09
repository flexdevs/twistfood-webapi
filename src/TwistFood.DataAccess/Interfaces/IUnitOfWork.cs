using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Interfaces.Categories;
using TwistFood.DataAccess.Interfaces.Discounts;
using TwistFood.DataAccess.Interfaces.Employees;
using TwistFood.DataAccess.Interfaces.Locations;
using TwistFood.DataAccess.Interfaces.Orders;
using TwistFood.DataAccess.Interfaces.Phones;
using TwistFood.DataAccess.Interfaces.Products;
using TwistFood.DataAccess.Interfaces.Users;
using TwistFood.Domain.Entities.Products;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Categories { get; }
        public IDiscountRepository Discounts { get; }
        public IAdminRepository Admins { get; }
        public IDeliverRepository Delivers { get; }
        public IOperatorRepository Operators { get; }
        public ILocationRepository Locations { get; }
        public IOrderDetailRepository OrderDetails { get; }
        public IOrderRepository Orders { get; }
        public IPhoneRepository Phones { get; }
        public IProductRepository Products { get; }
        public IUserRepository Users { get; }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        public Task<int> SaveChangesAsync();
    }
}
