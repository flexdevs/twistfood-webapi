using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Domain.Entities.Employees;

namespace TwistFood.DataAccess.Configurations
{
    public class SuperAdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasData(new Admin()
            {
                Id = 1,
                FirstName = "Azamjon",
                LastName = "Shaydullayev",
                Email = "Azamjon@gmail.com",
                IsHead = true,
                PhoneNumber = "+998942732650",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ImagePath = "",
                Salary = 3500,
                PassportSeriaNumber = "AD 1234567",
                PasswordHash = "$2a$12$QTe.2qNCBMiTaoZ2TeHuJuI6Tn5sknacPD0GISbqANErhSX.rSFsa",
                Salt = "9f8c6209-18f6-4628-83e5-110a2e8fe406"

            });
        }
    }
}
