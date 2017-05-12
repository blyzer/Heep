using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace HeeP.Data
{
    public interface IDbContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        
        int SaveChanges();
        
        IEnumerable<ModelValidator> GetValidationErrors();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        EntityEntry Entry(object entity);
        string ConnectionString { get; set; }
        bool AutoDetectChangedEnabled { get; set; }
        void ExecuteSqlCommand(string p, params object[] o);
        void ExecuteSqlCommand(string p);
    }
}
