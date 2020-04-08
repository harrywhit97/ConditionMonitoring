using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using ConditionMonitoringAPI.Features.Boards.Validators;
using ConditionMonitoringAPI.Features.Readings;
using ConditionMonitoringAPI.Features.Sensors;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using ConditionMonitoringAPI.Services;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.OpenApi.Models;

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

            services.AddMvc()
                .AddControllersAsServices()
                .AddJsonOptions(options => {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    options.JsonSerializerOptions.IgnoreNullValues = true;
                }); ;

            //Work around to enable swagger
            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });

            services.AddScoped<LightSensorReadingValidator>();
            services.AddScoped<BoardValidator>();
            services.AddScoped<SensorValidator>();

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddAutoMapper(typeof(FeaturesProfile));
            services.AddMediatR(Assembly.GetExecutingAssembly());
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

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.EnableDependencyInjection();
                endpoints.MapControllers();
                endpoints.Select().Filter().OrderBy().Count().MaxTop(10).Expand();
                endpoints.MapODataRoute("api", "api", GetEdmModel());
            });
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
