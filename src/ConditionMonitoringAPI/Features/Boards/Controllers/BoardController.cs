using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Boards.Commands;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Features.Boards.Controllers
{
    public class BoardController : AbstractController<Board, long, CreateBoard, UpdateBoard>
    {
        public BoardController(ConditionMonitoringDbContext context, ILogger<BoardController> logger)
            :base(context, logger)
        {
            //add get by ip
        }
    }
}
