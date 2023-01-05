using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Common.Interfaces;
using TwistFood.DataAccess.Common.Utils;
using TwistFood.DataAccess.Interfaces.Locations;
using TwistFood.Domain.Common;
using TwistFood.Domain.Exceptions;

namespace TwistFood.DataAccess.Repositories.Locations;

public class LocationRepository : GenericRepository<Location>, ILocationRepository
{ 
    public LocationRepository(AppDbContext appDbContext) : base(appDbContext)
    {

    }
}