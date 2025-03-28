﻿using Common.ServiceAttribute;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EntityCoreTemplate.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCustomServices();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
