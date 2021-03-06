using System;
using Application.Common.Interface;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class InfraInstaller
    {
        private static bool UseInMemory { get; set; } = false;
        
        public static IServiceCollection InstallInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            UseInMemory = configuration.GetValue<bool>("UseInMemory");

            if (UseInMemory)
            {
                services.AddDbContext<MajorContext>(conf =>
                {
                    conf.UseInMemoryDatabase("VSDatabase");
                });
            }
            else
            {
                var connStrng = configuration.GetConnectionString("Default");
                if (string.IsNullOrWhiteSpace(connStrng))
                    throw new ArgumentException("No connection Default found");

                services.AddDbContext<MajorContext>(conf =>
                {
                    conf.UseNpgsql(connStrng);
                });
            }
            
            services.AddScoped<IAppContext>(provider => provider.GetService<MajorContext>());

            return services;
        }
        public static void MigrateDatebase(MajorContext context, bool autoMigrate = false)
        {
            
            if (!UseInMemory && autoMigrate)
                context.Database.Migrate();
        }
    }
}