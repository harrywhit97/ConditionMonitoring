using AutoMapper;
using ConditionMonitoringAPI.Features.Boards.Dtos;
using ConditionMonitoringAPI.Features.Common.Commands;
using ConditionMonitoringAPI.Mapping;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Boards.Commands
{
    public class UpdateBoard : BoardDto, IRequest<Board>, IHasId<long>, IMapFrom<Board>
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
