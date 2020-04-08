using Domain.Interfaces;
using MediatR;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{
    public class UpdateEntityFromDto<T, TId, TDto> : IRequest<T>
        where T : class, IHasId<TId>
    {
        public TId Id { get; set; }
        public TDto Dto { get; set; }

        public UpdateEntityFromDto(TId id, TDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }
}
