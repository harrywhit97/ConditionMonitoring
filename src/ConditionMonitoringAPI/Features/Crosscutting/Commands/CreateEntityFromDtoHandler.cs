using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Utils;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{

    public abstract class CreateEntityFromDtoHandler<T, TId, TValidator, TDto> : AbstractRequestHandler<T, TId, CreateEntityFromDto<T, TId, TDto>>
        where T : class, IHasId<TId>
        where TValidator : AbstractValidatorWrapper<T>
    {
        readonly TValidator Validator;

        public CreateEntityFromDtoHandler(DbContext dbContext, ILogger logger, TValidator validator, IMapper mapper)
            : base(dbContext, logger, mapper)
        {
            Validator = validator;
        }

        public override async Task<T> Handle(CreateEntityFromDto<T, TId, TDto> request, CancellationToken cancellationToken)
        {
            Logger.LogDebug("Recieved request");
            var Dto = request.Dto ?? throw new RestException(HttpStatusCode.BadRequest, "Null Dto");
            T entity;
            try
            {
                Dto = SanatizeData.SanitizeStrings(Dto);
                
                entity = Mapper.Map<T>(Dto);

                ValidationUtils.ValidateEntity(Validator, entity);
            }
            catch(Exception e)
            {
                throw new RestException(HttpStatusCode.BadRequest, e.Message);
            }            

            Context.Set<T>().Add(entity);
            await Context.SaveChangesAsync();

            return entity;
        }
    }
}
