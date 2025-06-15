using EntityCoreTemplate.Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EntityCoreTemplate.Infrastructure
{
    public partial class EntityCoreTemplateDb
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EntityCoreTemplateDb(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Database.Migrate();
        }

        /// <summary>
        /// Save all changes with auditable information.
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            try
            {
                var entries = ChangeTracker
                    .Entries()
                    .Where(e =>
                        e.State == EntityState.Added || e.State == EntityState.Modified)
                    .Where(e =>
                        e.Entity is Auditable ||
                        (e.Entity.GetType().IsGenericType &&
                         e.Entity.GetType().GetGenericTypeDefinition() == typeof(Auditable<>)));

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
            catch(Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Get current user id from HttpContext.
        /// </summary>
        /// <returns></returns>
        private string GetUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext is null)
                return "System";

            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return userId ?? "System";

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Default database Sql Server
            optionsBuilder.UseSqlServer(connectionString: "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EntityCoreTemplateDb;");
            
            // Sqliteda filelarda saqlangani uchun va birnechta startup projectlar(EntityCoreTemplate.Api va EntityCoreTemplate.UI) bo'lgani uchun maqul topmadim.
            //optionsBuilder.UseSqlite($"Data Source=EntityCoreTemplateDb");
        }
    }
}
