using AutoMapper;
using ConditionMonitoringAPI.Features.Boards.Dtos;
using ConditionMonitoringAPI.Features.Common.Commands;
using ConditionMonitoringAPI.Mapping;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Boards.Commands
{
    public class CreateBoard : BoardDto, IRequest<Board>, IMapFrom<Board>
    {
    }

    public class CreateBoardHandler : CreateEntityFromRequestHandler<Board, long, CreateBoard>
    {
        public CreateBoardHandler(ConditionMonitoringDbContext context, IMapper mapper)
            :base(context, mapper)
        {
        }
    }
}
