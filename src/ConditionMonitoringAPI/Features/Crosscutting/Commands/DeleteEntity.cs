using Domain.Interfaces;
using MediatR;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{
    public class DeleteEntity<T, TId> : IRequest<T>
        where T : class, IHasId<TId>
    {
        public TId Id { get; set; }

        public DeleteEntity(TId id)
        {
            Id = id;
        }
    }
}
