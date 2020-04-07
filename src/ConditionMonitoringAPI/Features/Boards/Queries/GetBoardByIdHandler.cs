using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Domain.Models;
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Features.Boards.Queries
{
    public class GetBoardByIdHandler : GetByIdHandler<Board, long>
    {
        public GetBoardByIdHandler(ConditionMonitoringDbContext context, ILogger<GetBoardById> logger)
            :base(context, logger)
        {
        }
    }
}
