﻿using OnlineMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistFood.Domain.Common
{
    public class Location : BaseEntity
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

    }
}
