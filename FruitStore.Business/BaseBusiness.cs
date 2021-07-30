using System;
using System.Linq;
using System.Linq.Expressions;

using Microsoft.EntityFrameworkCore;
namespace FruitStore.Business
{
    public class BaseBusiness
    {
        private DbContext Connection { get; set; }

        #region Constructor
        public BaseBusiness(DbContext context)
        {
            this.Connection = context;
        }
        #endregion

        public virtual IQueryable<TEntity> Query<TEntity>() where TEntity : Entity.BaseEntity
        {
            DbSet<TEntity> dbSet = null;
            IQueryable<TEntity> query = null;

            var type = typeof(TEntity);

            dbSet = this.Connection.Set<TEntity>();
            if (type.BaseType.Name == "DefaultEntity")
            {
                var source = Expression.Parameter(typeof(TEntity), "x");
                var member = Expression.Property(source, "Deleted");
                var exp = Expression.Equal(member, Expression.Constant(false));
                var lamda = Expression.Lambda<Func<TEntity, bool>>(exp, source);
                query = dbSet.Where(lamda);
            }
            else
            {
                query = dbSet.AsNoTracking();
            }
            return query;
        }
    }
}