using Domain.Interfaces;
using MediatR;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{
    public class CreateEntityFromDto<T, TId, TDto> : IRequest<T>
        where T : class, IHasId<TId>
    {
        public TDto Dto { get; set; }

        public CreateEntityFromDto(TDto dto)
        {
            Dto = dto;
        }
    }
}
