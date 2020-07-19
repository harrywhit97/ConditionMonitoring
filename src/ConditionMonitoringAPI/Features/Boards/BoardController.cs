using ConditionMonitoringAPI.Features.Boards.Commands;
using Domain.Models;
using Microsoft.Extensions.Logging;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Boards
{
    public class BoardController : CrudController<Board, long, CreateBoard, UpdateBoard>
    {
        public BoardController(ConditionMonitoringDbContext context, ILogger<BoardController> logger)
            :base(context, logger)
        {
            //add get by ip
        }
    }
}
