using AutoMapper;
using ConditionMonitoringAPI.Features.Boards.Queries;
using ConditionMonitoringAPI.Features.Boards.Validators;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Boards
{
    public class BoardHandlers
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
                :base(context)
            {
            }
        }

        public class CreateBoardHandler : CreateEntityFromDtoHandler<Board, long, BoardValidator, BoardDto>
        {
            public CreateBoardHandler(ConditionMonitoringDbContext context, BoardValidator validator, IMapper mapper)
                : base(context, validator, mapper)
            {
            }
        }

        public class UpdateBoardHandler : UpdateEntityFromDtoHandler<Board, long, BoardValidator, BoardDto>
        {
            public UpdateBoardHandler(ConditionMonitoringDbContext context, BoardValidator validator, IMapper mapper)
                : base(context, validator, mapper)
            {
            }
        }

        public class DeleteBoardByIdHandler : DeleteEntityHandler<Board, long>
        {
            public DeleteBoardByIdHandler(ConditionMonitoringDbContext context)
                : base(context)
            {
            }
        }

        public class GetBoardByIpHandler : IRequestHandler<GetBoardByIp, Board>
        {
            readonly ConditionMonitoringDbContext Context;

            public GetBoardByIpHandler(ConditionMonitoringDbContext dbContext)
            {
                Context = dbContext;
            }

            public Task<Board> Handle(GetBoardByIp request, CancellationToken cancellationToken)
            {
                var ip = request.Ip;

                var board = Context.Set<Board>()
                    .Include(x => x.Sensors)
                    .Where(x => x.IpAddress == ip)
                    .FirstOrDefaultAsync();

                return board ?? throw new Exception($"A board that has an Ip address of {ip} was not found.");
            }
        }
    }
}
