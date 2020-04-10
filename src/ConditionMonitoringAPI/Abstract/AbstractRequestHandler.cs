using AutoMapper;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Abstract
{
    public abstract class AbstractRequestHandler<T, TId, TRequest, TResult> : IRequestHandler<TRequest, TResult>
        where T : class, IHasId<TId>
        where TRequest : IRequest<TResult>
        {

        readonly protected DbContext Context;
        readonly protected ILogger Logger;
        readonly protected IMapper Mapper;

        public AbstractRequestHandler(DbContext dbContext, ILogger logger, IMapper mapper)
        {
            Context = dbContext;
            Logger = logger;
            Mapper = mapper;
        }

        public virtual Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class AbstractRequestHandler<T, TId, TRequest> : AbstractRequestHandler<T, TId, TRequest, T>
        where T : class, IHasId<TId>
        where TRequest : IRequest<T>
    {
        public AbstractRequestHandler(DbContext dbContext, ILogger logger, IMapper mapper)
            :base(dbContext, logger, mapper)
        {
        }
    }
}
