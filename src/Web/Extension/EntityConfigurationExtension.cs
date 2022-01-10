using System;
using Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Web.Extension
{
    public static class EntityExtension
    {
        public static IServiceCollection AddEntityConfiguration(
            this IServiceCollection services,
            string defaultConnection)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(defaultConnection,
                    b =>
                    {
                        b.MigrationsHistoryTable("__efmigrationshistory");
                        b.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                    }
                ));

            return services;
        }
    }
}