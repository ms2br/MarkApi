using MarkAPI.CORE.Entities;
using MarkAPI.CORE.Entities.Common;
using MarkAPI.CORE.Entities.Identity;
using MarkAPI.DAL.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace MarkAPI.DAL.Context
{
    public class MarkDbContext : IdentityDbContext
    {
        public MarkDbContext(DbContextOptions options) : base(options) {}

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var builder = modelBuilder.Entity<IdentityUser>();
            
            builder.Ignore(x => x.PhoneNumber)
                .Ignore(x => x.PhoneNumberConfirmed);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entities = ChangeTracker.Entries<BaseEntity>();
            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                    entity.Entity.CreatTime = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
