using ConditionMonitoringAPI.Abstract;
using Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
