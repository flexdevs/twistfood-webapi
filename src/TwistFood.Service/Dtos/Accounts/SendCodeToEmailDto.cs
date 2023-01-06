﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Service.Attributes;

namespace TwistFood.Service.Dtos.Accounts
{
    public class SendCodeToEmailDto
    {
        [Required, Email]
        public string Email { get; set; } = string.Empty;
    }
}
