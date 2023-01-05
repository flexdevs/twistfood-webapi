using OnlineMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TwistFood.DataAccess.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T?> FindByIdAsync(long id);

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);

        public void Add(T entity);

        public void Delete(long id);

        public void Update(long id, T entity);
    }
}
