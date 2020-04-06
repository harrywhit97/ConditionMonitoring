using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Features.Boards.Validators;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Boards.Controllers
{
    public class BoardController : GenericController<Board, long, BoardValidator>
    {
        public BoardController(ConditionMonitoringDbContext conditionMonitoringDbContext, BoardValidator validator)
            :base(conditionMonitoringDbContext, validator)
        {

        }
    }
}
