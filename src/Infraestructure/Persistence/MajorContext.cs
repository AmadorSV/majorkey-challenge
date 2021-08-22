using Domain.Model;
using Infraestructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Persistence
{
    public class MajorContext : DbContext
    {
        public DbSet<ServiceRequest> ServiceRequests { get; set; }
        
        public MajorContext(DbContextOptions<MajorContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ServiceRequestConfig.Config(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

    }
}