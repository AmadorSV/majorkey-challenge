using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence.Configurations
{
    public static class ServiceRequestConfig
    {
        public static void Config(ModelBuilder builder)
        {
            builder.Entity<ServiceRequest>(conf =>
            {
                conf.HasKey(o => o.Id);

                conf.Property(o => o.BuildingCode)
                    .HasMaxLength(100)
                    .IsRequired();

                conf.Property(o => o.Description)
                    .HasMaxLength(200)
                    .IsRequired();

            });
        }
    }
}