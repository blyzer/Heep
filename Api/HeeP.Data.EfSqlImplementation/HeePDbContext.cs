using HeeP.Data.EntityFrameworkImplementation.EntityMaps;
using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;

namespace HeeP.Data.EfSqlImplementation
{

    public class HeePDbContext : DbContext, IDbContext
    {
        public HeePDbContext(DbContextOptions options) : base(options)
        {
            /*
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;*/
            ChangeTracker.AutoDetectChangesEnabled = true;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            
            IConfigurationRoot connectionStringConfig = builder.Build();



            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                // Add "appsettings.json" to bootstrap EF config.
                .AddJsonFile("appsettings.json")
                // Add the EF configuration provider, which will override any
                // config made with the JSON provider.
                /*.AddEntityFrameworkConfig(options =>
                    options.UseSqlServer(connectionStringConfig.GetConnectionString(
                        "HeePDbConectionString"))
                )*/
                .Build();

            optionsBuilder.UseSqlServer(connectionStringConfig.GetConnectionString(
                        "HeePDbConectionString"));

            ChangeTracker.AutoDetectChangesEnabled = true;
            
        }

        public IEnumerable<ModelValidator> GetValidationErrors()
        {
            return (Enumerable.Empty<ModelValidator>());
        }

        #region IDbContext
        public bool AutoDetectChangedEnabled
        {
            get => ChangeTracker.AutoDetectChangesEnabled;

            set => ChangeTracker.AutoDetectChangesEnabled = value;
        }

        public string ConnectionString
        {            
        
            get => Database.GetDbConnection().ConnectionString;

            set => Database.GetDbConnection().ConnectionString = value;
        }

        public void ExecuteSqlCommand(string sql, params object[] parameters)
        {
            Database.ExecuteSqlCommand(sql, parameters);
        }

        public void ExecuteSqlCommand(string sql)
        {
            Database.ExecuteSqlCommand(sql);
        }
        #endregion

        #region DbSets
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }

        #endregion

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added))
            {
                //modify entry.Entity here
                entry.OriginalValues["RowVersion"] = entry.CurrentValues["RowVersion"];

            }

            AutoDetectChangedEnabled = false;
            var result = base.SaveChanges();
            AutoDetectChangedEnabled = true;

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach(var property in entityType.GetProperties())
                {
                    var columnName = property.SqlServer().ColumnName;
                    if (columnName.Length > 30)
                    {
                        throw new InvalidOperationException("Column name is greater than 30 characters - " + columnName);
                    }
                }

                // Skip shadow types
                if (entityType.ClrType == null)
                    continue;

                entityType.Relational().TableName = entityType.ClrType.Name;
            }

            new RolMap(modelBuilder.Entity<Role>());
            new UserMap(modelBuilder.Entity<User>());            
        }
    }
}