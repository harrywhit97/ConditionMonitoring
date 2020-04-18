using ConditionMonitoringAPI.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Queries
{
    public abstract class GetEntityByIdHandler<T, TId> : IRequestHandler<GetEntityById<T, TId>, T>
        where T : class, IHasId<TId>
    {
        readonly DbContext Context;

        public GetEntityByIdHandler(DbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task<T> Handle(GetEntityById<T, TId> request, CancellationToken cancellationToken)
        {
            var result = await Context.Set<T>().FindAsync(request.Id);
            _ = result ?? throw new RestException(HttpStatusCode.NotFound, $"Could not find a {typeof(T).Name} with an Id of {request.Id}");
            return result;
        }
    }
}
