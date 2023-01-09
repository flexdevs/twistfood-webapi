using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TwistFood.DataAccess.Configurations;
using TwistFood.Domain.Common;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Discounts;
using TwistFood.Domain.Entities.Employees;
using TwistFood.Domain.Entities.Order;
using TwistFood.Domain.Entities.Phones;
using TwistFood.Domain.Entities.Products;
using TwistFood.Domain.Entities.Users;

namespace TwistFood.Api.DbContexts
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; } = default!;
        public virtual DbSet<Location> Locations { get; set; } = default!;
        public virtual DbSet<Phone> Phones { get; set; } = default!;
        public virtual DbSet<Category> Categories { get; set; } = default!;
        public virtual DbSet<Product> Products { get; set; } = default!;
        public virtual DbSet<Admin> Admins { get; set; } = default!;
        public virtual DbSet<Deliver> Delivers { get; set; } = default!;
        public virtual DbSet<Operator> Operators { get; set; } = default!;
        public virtual DbSet<Order> Orders { get; set; } = default!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = default!;
        public virtual DbSet<Discount> Discounts { get; set; } = default!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder); 
        }
    }
}
