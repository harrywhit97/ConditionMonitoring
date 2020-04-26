using AutoMapper;
using ConditionMonitoringAPI.Features.Boards.Dtos;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Interfaces;
using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Boards.Commands
{
    public class CreateBoard : BoardDto, IRequest<Board>, IMapToo<Board>
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
