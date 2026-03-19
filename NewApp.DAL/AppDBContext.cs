using Microsoft.EntityFrameworkCore;
using NewApp.Core.Entities;
using NewApp.Core.Models;
using NewApp.Core.Models.Common;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NewApp.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

       public DbSet<Book> Books { get; set; }
       public DbSet<Autors> Autors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(AppDbContext).GetMethod(nameof(ConfigureSoftDelete), System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).MakeGenericMethod(entityType.ClrType);
                    method.Invoke(this, [modelBuilder]);
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        private void ConfigureSoftDelete<TEntity>(ModelBuilder modelBuilder) where TEntity : AuditableEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }

        public override int SaveChanges()
        {
            ApplyAuditConfigurations();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditConfigurations();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditConfigurations()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is AuditableEntity auditableEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditableEntity.CreatedDate = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            auditableEntity.UpdateDate = DateTime.UtcNow;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            auditableEntity.IsDeleted = true;
                            auditableEntity.DeleteDate = DateTime.UtcNow;
                            break;
                    }
                }

                if (entry.Entity is NewApp.Core.Entities.BaseEntity baseEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            baseEntity.CreatedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Modified:
                            baseEntity.UpdatedAt = DateTime.UtcNow;
                            break;
                    }
                }
            }
        }
    }
}
