using OnlineMarket.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TwistFood.Api.DbContexts;
using TwistFood.DataAccess.Interfaces;

namespace TwistFood.DataAccess.Repositories
{
    public class GenericRepository<T> : BaseRepository<T>, IGenericRepository<T>
     where T : BaseEntity
    {
        public GenericRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<T> GetAll() => _dbSet;

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
            => _dbSet.Where(expression);
    }
}
