using System;
using Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure
{
    public static class InfraInstaller
    {
        public static IServiceCollection InstallInfraestructure(this IServiceCollection services, IConfiguration configuration)
        {
            var useInMemory = configuration.GetValue<bool>("UseInMemory");

            if (useInMemory)
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

            return services;
        }

    }
}