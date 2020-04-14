using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Exceptions;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{

    public abstract class DeleteEntityHandler<T, TId> : AbstractRequestHandler<T, TId, DeleteEntity<T, TId>, bool>
        where T : class, IHasId<TId>
    {
        public DeleteEntityHandler(DbContext dbContext, ILogger logger, IMapper mapper)
            : base(dbContext, logger, mapper)
        {
        }

        public override async Task<bool> Handle(DeleteEntity<T, TId> request, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Recieved request");
            var entity = await Context.Set<T>().FindAsync(request.Id);

            if (entity == null)
                throw new RestException(HttpStatusCode.NotFound, $"Could not find a {typeof(T).Name} with an Id of '{request.Id}'");

            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
            return true;
        }
    }
}
