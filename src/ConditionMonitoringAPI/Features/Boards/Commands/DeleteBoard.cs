using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Boards.Commands
{
    public class DeleteSensor
    {
        public class DeleteBoardHandler : DeleteEntityHandler<Board, long>
        {
            public DeleteBoardHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }
    }
}
