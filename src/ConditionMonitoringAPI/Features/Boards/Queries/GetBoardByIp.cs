using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Exceptions;
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
    public class GetBoardByIp : IRequest<Board>
    {
        public string Ip { get; set; }

        public GetBoardByIp(string ip)
        {
            Ip = ip;
        }

        public class GetBoardByIpHandler : IRequestHandler<GetBoardByIp, Board>
        {
            ConditionMonitoringDbContext Context;
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

                return board ?? throw new NotFoundException($"Could not find a Board with an IP of {request.Ip}");
            }
        }
    }
}
