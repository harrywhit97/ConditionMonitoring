using ConditionMonitoringAPI.Abstract;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Boards.Controllers
{
    public class BoardController : GenericController<Board, long, BoardDto>
    {
        public BoardController(ConditionMonitoringDbContext conditionMonitoringDbContext, IMediator mediator)
            :base(conditionMonitoringDbContext, mediator)
        {
        }
    }
}
