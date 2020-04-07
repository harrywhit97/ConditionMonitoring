using ConditionMonitoringAPI.Abstract;
using Domain.Models;
using FluentValidation;

namespace ConditionMonitoringAPI.Features.Boards.Validators
{
    public class BoardValidator : AbstractValidatorWrapper<Board>
    {
        public BoardValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .Length(1, 50);
        }
    }
}
