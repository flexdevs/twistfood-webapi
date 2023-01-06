using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TwistFood.Service.Dtos.Accounts
{
    public class EmailMessageDto
    {
        [Required]
        public string To { get; set; } = string.Empty;
        [Required]
        public int Body { get; set; }
        [Required]
        public string Subject { get; set; } = string.Empty;
    }
}
