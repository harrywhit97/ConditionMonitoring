using ConditionMonitoringAPI.Abstract;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Queries
{
    public class GetById<T, TId> : IRequest<T> 
        where T : class, IHaveId<TId>
    {
        public TId Id { get; set; }

        public GetById(TId id)
        {
            Id = id;
        }
    }
}
