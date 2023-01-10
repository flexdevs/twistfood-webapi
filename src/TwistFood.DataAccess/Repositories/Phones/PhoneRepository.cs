using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces.Categories;
using TwistFood.DataAccess.Interfaces.Phones;
using TwistFood.Domain.Entities.Categories;
using TwistFood.Domain.Entities.Phones;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Repositories.Phones
{
    public class PhoneRepository : GenericRepository<Phone>, IPhoneRepository
    {
        public PhoneRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
    }
}
