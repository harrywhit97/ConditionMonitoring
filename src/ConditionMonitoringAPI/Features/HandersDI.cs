using ConditionMonitoringAPI.Features.Boards.Validators;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Features.Readings.Commands;
using ConditionMonitoringAPI.Features.SensorsReadings.Validators;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.Design;

namespace ConditionMonitoringAPI.Features
{
    public class HandersDI
    {
        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<CreateEntityHandler<LightSensorReading, long, LightSensorReadingValidator>>();
            services.AddTransient<CreateEntityHandler<Board, long, BoardValidator>>();

            services.AddTransient<GetById<Board, long>>();

            services.AddTransient<CreateReadingHandler>();
            return services;
        }
    }
}
