using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Utils;
using Domain.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting
{
    //Not functional...
    public abstract class BaseHandlers<T, TId, TValidator, TDto>
        where T : class, IHasId<TId>
        where TValidator : AbstractValidatorWrapper<T>
    {
        public class GetEntityByIdHandler : AbstractRequestHandler<T, TId, GetEntityById<T, TId>>
        {
            public GetEntityByIdHandler(ConditionMonitoringDbContext dbContext, ILogger logger, IMapper mapper)
                : base(dbContext, logger, mapper)
            {
            }

            public override async Task<T> Handle(GetEntityById<T, TId> request, CancellationToken cancellationToken)
            {
                var result = await Context.Set<T>().FindAsync(request.Id);
                _ = result ?? throw new Exception($"Could not find a {typeof(T).Name} with an Id of {request.Id}");
                return result;
            }
        }

        public abstract class CreateEntityFromDtoHandler : AbstractRequestHandler<T, TId, CreateEntityFromDto<T, TId, TDto>>
        {
            readonly TValidator Validator;

            public CreateEntityFromDtoHandler(ConditionMonitoringDbContext dbContext, ILogger logger, TValidator validator, IMapper mapper)
                : base(dbContext, logger, mapper)
            {
                Validator = validator;
            }

            public override async Task<T> Handle(CreateEntityFromDto<T, TId, TDto> request, CancellationToken cancellationToken)
            {
                var Dto = request.Dto ?? throw new RestException(HttpStatusCode.BadRequest, "Null Dto");
                T entity;
                try
                {
                    Dto = SanatizeData.SanitizeStrings(Dto);

                    entity = Mapper.Map<T>(Dto);

                    ValidationUtils.ValidateEntity(Validator, entity);
                }
                catch (Exception e)
                {
                    throw new RestException(HttpStatusCode.BadRequest, e.Message);
                }

                Context.Set<T>().Add(entity);
                await Context.SaveChangesAsync();

                return entity;
            }
        }

        public abstract class UpdateEntityFromDtoHandler : AbstractRequestHandler<T, TId, UpdateEntityFromDto<T, TId, TDto>>
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
                    request.Dto = SanatizeData.SanitizeStrings(request.Dto);

                    entity = Mapper.Map<T>(request.Dto);
                    entity.Id = request.Id;

                    ValidationUtils.ValidateEntity(Validator, entity);

                    Context.Entry(entity).State = EntityState.Modified;
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException e)
                {
                    _ = await Context.Set<T>().FindAsync(request.Id)
                        ?? throw new RestException(HttpStatusCode.NotFound, $"Could not find a {typeof(T).Name} with an Id of {request.Id}");

                    throw;
                }
                catch (Exception e)
                {
                    throw new RestException(HttpStatusCode.BadRequest, e.Message);
                }
                return entity;
            }
        }

        public abstract class DeleteEntityHandler : AbstractRequestHandler<T, TId, DeleteEntity<T, TId>, bool>
        {
            public DeleteEntityHandler(ConditionMonitoringDbContext dbContext, ILogger logger, IMapper mapper)
                : base(dbContext, logger, mapper)
            {
            }

            public override async Task<bool> Handle(DeleteEntity<T, TId> request, CancellationToken cancellationToken)
            {
                var entity = await Context.Set<T>().FindAsync(request.Id);

                if (entity == null)
                    throw new RestException(HttpStatusCode.NotFound, $"Could not find a {typeof(T).Name} with an Id of '{request.Id}'");

                Context.Set<T>().Remove(entity);
                Context.SaveChanges();
                return true;
            }
        }
    }
}
