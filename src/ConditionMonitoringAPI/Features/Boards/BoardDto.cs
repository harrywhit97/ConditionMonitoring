using Domain.Models;
using WebApiUtilities.Abstract;

namespace ConditionMonitoringAPI.Features.Boards
{
    public class BoardDto : Dto<Board, long>
    {
        public string Name { get; set; }
        public string IpAddress { get; set; }
    }
}
