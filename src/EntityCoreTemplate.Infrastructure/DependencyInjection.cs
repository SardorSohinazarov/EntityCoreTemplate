using Microsoft.Extensions.DependencyInjection;

namespace EntityCoreTemplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<EntityCoreTemplateDbContext>();

            return services;
        }
    }
}
