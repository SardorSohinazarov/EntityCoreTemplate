using EntityCoreTemplate.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EntityCoreTemplate.Infrastructure
{
    public partial class EntityCoreTemplateDb : DbContext
    {
        // tables
        public DbSet<Book> Books { get; set; }
    }
}
