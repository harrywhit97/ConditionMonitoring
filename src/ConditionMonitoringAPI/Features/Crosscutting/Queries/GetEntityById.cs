using Domain.Interfaces;
using MediatR;

namespace ConditionMonitoringAPI.Features.Crosscutting.Queries
{
    public class GetEntityById<T, TId> : IRequest<T> 
        where T : class, IHasId<TId>
    {
        public TId Id { get; set; }

        public GetEntityById(TId id)
        {
            Id = id;
        }
    }
}
