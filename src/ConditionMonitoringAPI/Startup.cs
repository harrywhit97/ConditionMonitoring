using System.Linq;
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
using WebApiUtilities.Extenstions;

namespace ConditionMonitoringAPI
{
    public class Startup
    {
        const int apiVersion = 1;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            var connectionString = Configuration.GetConnectionString("Database");

            services.AddDbContext<ConditionMonitoringDbContext>(options =>
                options.UseInMemoryDatabase(""));

            services.AddWebApiServices("Condition Monitoring API", apiVersion);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseRouting();

            app.UseAuthorization();

            app.AddWebApiUtilities(GetEdmModel(), 10, apiVersion);
        }

        IEdmModel GetEdmModel()
        {
            var odataBuilder = new ODataConventionModelBuilder();
            odataBuilder.EntitySet<LightSensorReading>(nameof(LightSensorReading));
            odataBuilder.EntitySet<Sensor<ISensorReading>>(nameof(Sensor<ISensorReading>));
            odataBuilder.EntitySet<Board>(nameof(Board));
            return odataBuilder.GetEdmModel();
        }
    }
}
