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
        public class GetBoardByIdHandler : GetByIdHandler<Board, long>
        {
            public GetBoardByIdHandler(ConditionMonitoringDbContext context, ILogger<GetById<Board, long>> logger)
                : base(context, logger)
            {
            }
        }

        public class CreateBoardHandler : CreateEntityHandler<Board, long, BoardValidator>
        {
            public CreateBoardHandler(ConditionMonitoringDbContext context, ILogger<CreateBoardHandler> logger, BoardValidator validator)
                : base(context, logger, validator)
            {
            }
        }

        public class DeleteBoardHandler : DeleteEntityHandler<Board, long>
        {
            public DeleteBoardHandler(ConditionMonitoringDbContext context, ILogger<DeleteBoardHandler> logger)
                : base(context, logger)
            {
            }
        }

        public class UpdateBoardHandler : UpdateEntityHandler<Board, long, BoardValidator>
        {
            public UpdateBoardHandler(ConditionMonitoringDbContext context, ILogger<UpdateBoardHandler> logger, BoardValidator validator)
                : base(context, logger, validator)
            {
            }
        }


        public class GetBoardByIpHandler : AbstractRequestHandler<Board, long, GetBoardByIp>
        {
            public GetBoardByIpHandler(ConditionMonitoringDbContext dbContext, ILogger<GetBoardByIpHandler> logger)
                : base(dbContext, logger)
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
