using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Utils;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{
    public class CreateEntity<T, TId> : IRequest<T>
        where T : class, IHaveId<TId>
    {
        public T Entity { get; set; }

        public CreateEntity(T entity)
        {
            Entity = entity;
        }
    }
}
