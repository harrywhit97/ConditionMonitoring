using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Abstract
{
    public abstract class AbstractRequestHandler<T, TId, TRequest> : IRequestHandler<TRequest, T>
        where T : class, IHaveId<TId>
        where TRequest : IRequest<T>
        {

        readonly protected DbContext Context;
        readonly protected ILogger Logger;

        public AbstractRequestHandler(DbContext dbContext, ILogger logger)
        {
            Context = dbContext;
            Logger = logger;
        }

        public virtual Task<T> Handle(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
