using FluentValidation;

namespace ConditionMonitoringAPI.Features.Boards.Commands
{
    public class UpdateBoardValidator : AbstractValidator<UpdateBoard>
    {
        public UpdateBoardValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 64);

            RuleFor(x => x.IpAddress)
                .Length(5, 10);
        }
    }
}
