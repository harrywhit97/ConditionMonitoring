using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Boards.Validators;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Boards.Controllers
{
    public class BoardController : GenericController<Board, long, BoardValidator>
    {
        public BoardController(ConditionMonitoringDbContext conditionMonitoringDbContext, BoardValidator validator, IMediator mediator)
            :base(conditionMonitoringDbContext, validator, mediator)
        {
        }
    }
}
