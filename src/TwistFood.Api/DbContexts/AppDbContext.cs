using Microsoft.EntityFrameworkCore;
using System.Drawing;
using TwistFood.Api.Models;

namespace TwistFood.Api.DbContexts
{
    public class AppDbContext : DbContext
    {
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
            string CONNECTIONSTRING = "Host=localhost; Port=5432; Database=TwistFood-db; User Id=postgres; Password=1212;";
            optionsBuilder.UseNpgsql(CONNECTIONSTRING);
           
        }
    }
}
