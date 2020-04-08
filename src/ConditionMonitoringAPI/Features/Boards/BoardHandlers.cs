using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Boards.Queries;
using ConditionMonitoringAPI.Features.Boards.Validators;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Boards
{
    public class BoardHandlers
    {
        public class GetBoardByIdHandler : GetEntityByIdHandler<Board, long>
        {
            public GetBoardByIdHandler(ConditionMonitoringDbContext context, ILogger logger, IMapper mapper)
                :base(context, logger, mapper)
            {
            }
        }

        public class CreateBoardHandler : CreateEntityFromDtoHandler<Board, long, BoardValidator, BoardDto>
        {
            public CreateBoardHandler(ConditionMonitoringDbContext context, ILogger logger, BoardValidator validator, IMapper mapper)
                : base(context, logger, validator, mapper)
            {
            }
        }

        public class UpdateBoardHandler : UpdateEntityFromDtoHandler<Board, long, BoardValidator, BoardDto>
        {
            public UpdateBoardHandler(ConditionMonitoringDbContext context, ILogger logger, BoardValidator validator, IMapper mapper)
                : base(context, logger, validator, mapper)
            {
            }
        }

        public class DeleteBoardByIdHandler : DeleteEntityHandler<Board, long>
        {
            public DeleteBoardByIdHandler(ConditionMonitoringDbContext context, ILogger logger, IMapper mapper)
                : base(context, logger, mapper)
            {
            }
        }

        public class GetBoardByIpHandler : AbstractRequestHandler<Board, long, GetBoardByIp>
        {
            public GetBoardByIpHandler(ConditionMonitoringDbContext dbContext, ILogger logger, IMapper mapper)
                : base(dbContext, logger, mapper)
            {
            }

            public override Task<Board> Handle(GetBoardByIp request, CancellationToken cancellationToken)
            {
                Logger.LogInformation($"{nameof(GetBoardByIpHandler)} recieved request");

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
