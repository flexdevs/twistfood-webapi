﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.DataAccess.Common.Utils;
using TwistFood.Domain.Entities.Employees;

namespace TwistFood.DataAccess.Interfaces.Employees;

public interface IAdminRepository : IGenericRepository<Admin>
{
}
