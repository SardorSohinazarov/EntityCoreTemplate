using EntityCoreTemplate.Domain.Common;
using EntityCoreTemplate.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EntityCoreTemplate.Infrastructure
{
    public class EntityCoreTemplateDb : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EntityCoreTemplateDb(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Book> Books { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker.Entries()
                .Where(e => (e.Entity.GetType() == typeof(Auditable)
                            || (e.Entity.GetType().IsGenericType && e.Entity.GetType().GetGenericTypeDefinition() == typeof(Auditable<>)))
                            && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                dynamic auditableEntity = entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    auditableEntity.Created = DateTime.UtcNow;
                    auditableEntity.CreatedBy = GetUserId();
                }

                if (entry.State == EntityState.Modified)
                {
                    auditableEntity.LastModified = DateTime.UtcNow;
                    auditableEntity.LastModifiedBy = GetUserId();
                }
            }

            return await base.SaveChangesAsync();
        }

        private async Task<string> GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null)
                return "System";

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId ?? "System";

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source=EntityCoreTemplateDb");
        }
    }
}
