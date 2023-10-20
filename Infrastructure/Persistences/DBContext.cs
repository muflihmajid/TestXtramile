using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SceletonAPI.Application.Interfaces;
using SceletonAPI.Domain.Entities;
using SceletonAPI.Domain.Infrastructures;
using StoredProcedureEFCore;


namespace SceletonAPI.Infrastructure.Persistences {
    public class DBContext : DbContext, IDBContext {
        public DBContext (DbContextOptions<DBContext> options) : base (options) {

        }

        public DbSet<User> Users { set; get; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DBContext).Assembly);
        }

        public override EntityEntry Add(object entity)
        {
            if (entity is BaseEntity)
            {
                ((BaseEntity)entity).RowStatus = 0;
            }

            return base.Add(entity);
        }

        public IStoredProcBuilder loadStoredProcedureBuilder(string val)
        {
            return this.LoadStoredProc(val);
        }

        public override int SaveChanges()
        {
            this.HandleSave();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            this.HandleSave();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            this.HandleSave();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void HandleSave()
        {

        }

        private void UpdateSoftDeleteStatuses(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ((BaseEntity)entry.Entity).RowStatus = 0;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    ((BaseEntity)entry.Entity).RowStatus = -1;
                    break;
            }
        }
        public Task<int> SaveChangesAsync1()
        {
            this.HandleSave();
            return base.SaveChangesAsync(default);
        }
    }
}
