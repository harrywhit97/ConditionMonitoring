using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Utils;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using AutoMapper;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{

    public abstract class UpdateEntityFromDtoHandler<T, TId, TValidator, TDto> : AbstractRequestHandler<T, TId, UpdateEntityFromDto<T, TId, TDto>>
        where T : class, IHasId<TId>
        where TValidator : AbstractValidatorWrapper<T>
    {
        readonly TValidator Validator;

        public UpdateEntityFromDtoHandler(ConditionMonitoringDbContext dbContext, ILogger logger, TValidator validator, IMapper mapper)
            : base(dbContext, logger, mapper)
        {
            Validator = validator;
        }

        public override async Task<T> Handle(UpdateEntityFromDto<T, TId, TDto> request, CancellationToken cancellationToken)
        {
            T entity;
            try
            {
                var e = await Context.Set<T>().FindAsync(request.Id)
                   ?? throw new RestException(HttpStatusCode.NotFound, $"Could not find a {typeof(T).Name} with an Id of {request.Id}");

                Context.Entry(e).State = EntityState.Detached;

                request.Dto = SanatizeData.SanitizeStrings(request.Dto);

                entity = Mapper.Map<T>(request.Dto);
                entity.Id = request.Id;

                ValidationUtils.ValidateEntity(Validator, entity);

                Context.Entry(entity).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                throw new RestException(HttpStatusCode.BadRequest, e.Message);
            }
            return entity;
        }
    }
}
