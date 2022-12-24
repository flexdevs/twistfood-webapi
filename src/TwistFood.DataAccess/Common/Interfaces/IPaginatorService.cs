using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwistFood.DataAccess.Common.Interfaces
{
    public interface IPaginatorService
    {
        public  Task<IList<T>> ToPagedAsync<T>(IQueryable<T> items, int pageNumber, int pageSize); 
    }
}
