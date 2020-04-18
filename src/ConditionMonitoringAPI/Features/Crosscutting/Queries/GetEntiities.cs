using Domain.Interfaces;
using MediatR;
using System.Linq;

namespace ConditionMonitoringAPI.Features.Crosscutting.Queries
{
    public class GetEntiities<T, TId> : IRequest<IQueryable<T>> 
        where T : class, IHasId<TId>
    {
        public GetEntiities()
        {
        }
    }
}
