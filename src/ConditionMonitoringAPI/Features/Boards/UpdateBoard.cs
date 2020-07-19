using Domain.Models;
using WebApiUtilities.CrudRequests;

namespace ConditionMonitoringAPI.Features.Boards
{
    public class UpdateBoard : BoardDto, IUpdateCommand<Board, long>
    {
    }
}
