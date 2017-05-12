using System;
using System.Linq;
using System.Linq.Expressions;
using HeeP.Core.Repository;
using HeeP.Data;
using Microsoft.EntityFrameworkCore;

namespace HeeP.Core.Implementation.Repository
{
    public abstract class BaseRepository<T> : IRepository<T>
         where T : class
    {
        private readonly IDbContext _db;
        public IDbContext Db => _db;

        public BaseRepository(IDbContext context)
        {
            _db = context;
        }

        public virtual void Add(T item)
        {
            _db.Set<T>().Add(item);
        }

        public virtual T Get(params object[] arguments)
        {
            return _db.Set<T>().Find(arguments);
        }

        public virtual IQueryable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public virtual IQueryable<T> GetFiltered(Expression<Func<T, bool>> filter)
        {

            return _db.Set<T>().Where(filter);
        }

        public virtual IQueryable<T> GetPaged<Property>(int pageIndex, int pageCount, Expression<Func<T, Property>> orderByExpression, bool ascending)
        {
            if (ascending)
            {
                return _db.Set<T>().OrderBy(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
            else
            {
                return _db.Set<T>().OrderByDescending(orderByExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
            }
        }


        public virtual void Modify(T item)
        {
            var entry = _db.Entry(item);
            if (entry.State == EntityState.Detached)
            {
                _db.Set<T>().Attach(item);
                entry = _db.Entry(item);
            }

            entry.State = EntityState.Modified;
        }

        public virtual int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public virtual void Dispose()
        {
            _db.Dispose();
        }
    }
}
