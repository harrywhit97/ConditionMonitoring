using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Boards.Queries
{
    public class GetBoardQueries
    {
        public class GetBoardsHandler : GetEntitiesHandler<Board, long>
        {
            public GetBoardsHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }

        public class GetBoardByIdHandler : GetEntityByIdHandler<Board, long>
        {
            public GetBoardByIdHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }
    }
}
