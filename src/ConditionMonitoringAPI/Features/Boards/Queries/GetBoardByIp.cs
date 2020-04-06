using Domain.Models;
using MediatR;

namespace ConditionMonitoringAPI.Features.Boards.Queries
{
    public class GetBoardByIp : IRequest<Board>
    {
        public string Ip { get; set; }

        public GetBoardByIp(string ip)
        {
            Ip = ip;
        }
    }
}
