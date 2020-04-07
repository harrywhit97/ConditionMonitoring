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

        public class GetByIdHandler<T, TId> : AbstractRequestHandler<T, TId, GetById<T, TId>>
            where T : class, IHaveId<TId>
                {
            public GetByIdHandler(DbContext dbContext, ILogger logger)
                : base(dbContext, logger)
            {
            }

            public override async Task<T> Handle(GetById<T, TId> request, CancellationToken cancellationToken)
            {
                var result = await Context.Set<T>().FindAsync(request.Id);
                _ = result ?? throw new Exception($"Could not find a {typeof(T).Name} with an Id of {request.Id}");
                return result;
            }
        }
    }
}
