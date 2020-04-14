using ConditionMonitoringAPI.Abstract;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Features.Boards.Controllers
{
    public class BoardController : AbstractController<Board, long, BoardDto>
    {
        public BoardController(ConditionMonitoringDbContext conditionMonitoringDbContext, 
            IMediator mediator, 
            ILogger<BoardController> logger)
            :base(conditionMonitoringDbContext, mediator, logger)
        {
        }
    }
}
