using ConditionMonitoringAPI.Abstract;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Boards.Queries
{
    public class GetBoardByIp : IRequest<Board>
    {
        public string Ip { get; set; }

        public GetBoardByIp(string ip)
        {
            Ip = ip;
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

                var board = Context.Set<Board>().Include(x => x.Sensors)
                    .Where(x => x.IpAddress == ip).FirstOrDefaultAsync();

                return board ?? throw new Exception($"A board that has an Ip address of {ip} was not found.");
            }
        }
    }
}
