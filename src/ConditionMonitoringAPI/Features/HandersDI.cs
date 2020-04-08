using ConditionMonitoringAPI.Features.Boards;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ConditionMonitoringAPI.Features
{
    public class HandersDI
    {
        public IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(BoardHandlers).Assembly);
            return services;
        }
    }
}
