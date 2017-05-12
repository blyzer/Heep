using HeeP.Data;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace HeeP.Core.Repository
{
    /// <summary>
    /// Definte the behaivor of CRUD implementation  of an entity of type T
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> : IDisposable
        where T : class
    {
        /// <summary>
        /// Add the entity of T to the data repository
        /// </summary>
        /// <param name="item">entity to be added</param>
        void Add(T item);

        /// <summary>
        /// Get an entity of T whith the specify primary key
        /// </summary>
        /// <param name="arguments">List of the primary key values</param>
        /// <returns></returns>
        T Get(params object[] arguments);

        /// <summary>
        /// Get all entityes of T
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Get a Collection of T entityes that met te filter criteria
        /// </summary>
        /// <param name="filter">filter cirteria</param>
        /// <returns>Collection of T entities</returns>
        IQueryable<T> GetFiltered(Expression<Func<T, bool>> filter);

        IQueryable<T> GetPaged<Property>(int pageIndex, int pageCount,
           Expression<Func<T, Property>> orderByExpression, bool ascending);

        /// <summary>
        /// Update the specified T entity 
        /// </summary>
        /// <param name="item">Entity to update</param>
        void Modify(T item);

        /// <summary>
        /// Save/persist the uncommited changes to the repositori
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        IDbContext Db { get; }
    }
}
