using FluentValidation;

namespace ConditionMonitoringAPI.Features.Boards.Commands
{
    public class CreateBoardValidator : AbstractValidator<CreateBoard>
    {
        public CreateBoardValidator()
        {
            RuleFor(x => x.Name)
                .Length(1, 64);

            RuleFor(x => x.IpAddress)
                .Length(5, 10);
        }
    }
}
