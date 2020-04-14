using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Queries
{
    public abstract class GetEntityByIdHandler<T, TId> : AbstractRequestHandler<T, TId, GetEntityById<T, TId>>
        where T : class, IHasId<TId>
            {
        public GetEntityByIdHandler(DbContext dbContext, ILogger logger, IMapper mapper)
            : base(dbContext, logger, mapper)
        {
        }

        public override async Task<T> Handle(GetEntityById<T, TId> request, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Recieved request");
            var result = await Context.Set<T>().FindAsync(request.Id);
            _ = result ?? throw new RestException(HttpStatusCode.NotFound , $"Could not find a {typeof(T).Name} with an Id of {request.Id}");
            return result;
        }
    }
}
