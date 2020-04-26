using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Queries
{
    public class GetEntiities<T, TId> : IRequest<IQueryable<T>> 
        where T : class, IHasId<TId>
    {
    }

    public abstract class GetEntitiesHandler<T, TId> : IRequestHandler<GetEntiities<T, TId>, IQueryable<T>>
        where T : class, IHasId<TId>
    {
        readonly DbContext Context;

        public GetEntitiesHandler(DbContext dbContext)
        {
            Context = dbContext;
        }

        public Task<IQueryable<T>> Handle(GetEntiities<T, TId> request, CancellationToken cancellationToken)
        {
            return (Task<IQueryable<T>>)Context.Set<T>().AsQueryable();
        }
    }
}
