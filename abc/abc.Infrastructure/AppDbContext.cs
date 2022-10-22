using abc.core.Domain;
using abc.core.Entities;
using Microsoft.EntityFrameworkCore;

namespace abc.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entity in ChangeTracker.Entries<IAudit>())
            {
                if (entity.State is EntityState.Added or EntityState.Modified)
                {
                    if (entity.State == EntityState.Modified)
                    {
                        entity.Entity.SetModifiedOn(DateTime.UtcNow);
                    }

                    if (entity.State == EntityState.Added)
                    {
                        entity.Entity.SetCreatedOn(DateTime.UtcNow);
                    }
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasKey(a => a.Id);
        }
    }
}
