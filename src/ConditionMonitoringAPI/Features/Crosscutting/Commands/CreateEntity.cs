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

        public class CreateEntityHandler<T, TId, TValidator> : AbstractRequestHandler<T, TId, CreateEntity<T, TId>>
            where T : class, IHaveId<TId>
            where TValidator : AbstractValidatorWrapper<T>
        {
            readonly TValidator Validator;

            public CreateEntityHandler(DbContext dbContext, ILogger logger, TValidator validator)
                : base(dbContext, logger)
            {
                Validator = validator;
            }

            public override async Task<T> Handle(CreateEntity<T, TId> request, CancellationToken cancellationToken)
            {
                var entity = request.Entity;

                entity = SanatizeData.SanitizeStrings(entity);
                ValidationUtils.ValidateEntity(Validator, entity);

                Context.Set<T>().Add(entity);
                await Context.SaveChangesAsync();

                return entity;
            }
        }
    }
}
