using AutoMapper;
using ConditionMonitoringAPI.Features.Boards.Dtos;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using Domain.Models;

namespace ConditionMonitoringAPI.Features.Boards.Commands
{
    public class UpdateBoard : BoardDto, IUpdateEntityFromRequest<Board, long>
    {
        public long Id { get; set; }
    }

    public class UpdateBoardHandler : UpdateEntityFromRequestHandler<Board, long, UpdateBoard>
    {
        public UpdateBoardHandler(ConditionMonitoringDbContext context, IMapper mapper)
            :base(context, mapper)
        {
        }
    }
}
