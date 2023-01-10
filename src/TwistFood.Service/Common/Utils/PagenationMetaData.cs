using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistFood.Service.Common.Utils
{
    public class PagenationMetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}
