using FluentValidation;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Boards.Commands
{
    public class BoardDtoValidator<TDto> : DtoValidator<TDto, BoardDto>
        where TDto : BoardDto
    {
        public BoardDtoValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 64);

            RuleFor(x => x.IpAddress)
                .Length(5, 10);
        }
    }
}
