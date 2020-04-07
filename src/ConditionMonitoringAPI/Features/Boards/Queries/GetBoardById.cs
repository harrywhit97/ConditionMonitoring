using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Boards.Queries
{
    public class GetBoardById : GetById<Board, long>
    {
        public GetBoardById(long Id)
            :base(Id)
        {
        }
    }
}
