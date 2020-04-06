using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Boards.Queries
{
    public class GetBoardByIpHandler : IRequestHandler<GetBoardByIp, Board>
    {
        readonly ILogger Logger;
        readonly ConditionMonitoringDbContext Context;

        public GetBoardByIpHandler(ConditionMonitoringDbContext dbContext, ILogger logger)
        {
            Context = dbContext;
            Logger = logger;
        }

        public Task<Board> Handle(GetBoardByIp request, CancellationToken cancellationToken)
        {
            Logger.LogInformation($"{nameof(GetBoardByIpHandler)} recieved request");

            var ip = request.Ip;

            var board = Context.Set<Board>().Include(x => x.Sensors)
                .Where(x => x.IpAddress == ip).FirstOrDefaultAsync();

            if(board is null)
                throw new Exception($"A board that has an Ip address of {ip} was not found.");

            return board;
        }
    }
}
