using System.Linq;
using ConditionMonitoringAPI.Services;
using ConditionMonitoringAPI.Validators;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OData.Edm;

namespace ConditionMonitoringAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            var connectionString = Configuration.GetConnectionString("Database");

            services.AddDbContext<ConditionMonitoringDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddOData();

            services.AddMvc().AddControllersAsServices();

            services.AddScoped<LightSensorReadingValidator>();

            services.AddTransient<IDateTime, DateTimeService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.Select().Filter().OrderBy().Count().MaxTop(10).Expand();
                endpoints.MapODataRoute("api", "api", GetEdmModel());
            });
        }

        IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<LightSensorReading>(nameof(LightSensorReading));
            odataBuilder.EntitySet<LightSensor>(nameof(LightSensor));
            odataBuilder.EntitySet<Board>(nameof(Board));
            return odataBuilder.GetEdmModel();
        }
    }
}
