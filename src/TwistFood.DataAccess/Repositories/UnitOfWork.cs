using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces;
using TwistFood.DataAccess.Interfaces.Categories;
using TwistFood.DataAccess.Interfaces.Discounts;
using TwistFood.DataAccess.Interfaces.Employees;
using TwistFood.DataAccess.Interfaces.Locations;
using TwistFood.DataAccess.Interfaces.Orders;
using TwistFood.DataAccess.Interfaces.Phones;
using TwistFood.DataAccess.Interfaces.Products;
using TwistFood.DataAccess.Interfaces.Users;
using TwistFood.DataAccess.Repositories.Categories;
using TwistFood.DataAccess.Repositories.Discounts;
using TwistFood.DataAccess.Repositories.Employees;
using TwistFood.DataAccess.Repositories.Locations;
using TwistFood.DataAccess.Repositories.Orders;
using TwistFood.DataAccess.Repositories.Phones;
using TwistFood.DataAccess.Repositories.Products;
using TwistFood.DataAccess.Repositories.Users;

namespace TwistFood.DataAccess.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext dbContext;
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

        public UnitOfWork(AppDbContext appDbContext)
        {
            dbContext = appDbContext;
            Categories = new CategoryRepository(appDbContext);
            Discounts = new DiscountRepository(appDbContext);
            Admins = new AdminRepository(appDbContext);
            Delivers = new DeliverRepository(appDbContext);
            Operators = new OperatorRepository(appDbContext);
            Locations = new LocationRepository(appDbContext);
            OrderDetails = new OrderDetailRepository(appDbContext);
            Orders = new OrderRepository(appDbContext);
            Phones = new PhoneRepository(appDbContext);
            Products = new ProductRepository(appDbContext);
            Users = new UserRepository(appDbContext);

        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return dbContext.Entry(entity);
        }
    }
}
