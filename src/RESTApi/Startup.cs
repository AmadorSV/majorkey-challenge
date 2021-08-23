using Application;
using FluentValidation.AspNetCore;
using Infraestructure;
using Infraestructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using RESTApi.Filters;

namespace RESTApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(x =>
            {
                x.Filters.Add<ValidationFilter>();
            }).AddFluentValidation();
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Major/Cohesion RESTApi Challenge", Version = "v1" });
            });
    
            services.InstallInfraestructure(Configuration);
            services.InstallApplication();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, MajorContext context,ILogger<Startup> logger, IConfiguration configuration)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESTApi v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            //AutoMigrate Database
            var autoMigrate = configuration.GetValue<bool>("AutoMigrateDatabase");
            if (autoMigrate)
            {
                logger.LogInformation("Migrating Database");
                InfraInstaller.MigrateDatebase(context,true);
            }
        }
    }
}
