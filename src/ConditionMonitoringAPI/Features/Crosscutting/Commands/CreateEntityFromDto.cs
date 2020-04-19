using AutoMapper;
using ConditionMonitoringAPI.Abstract;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Utils;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{
    public class CreateEntityFromDto<T, TId, TDto> : IRequest<T>
        where T : class, IHasId<TId>
    {
        public TDto Dto { get; set; }

        public CreateEntityFromDto(TDto dto)
        {
            Dto = dto;
        }
    }

    public abstract class CreateEntityFromDtoHandler<T, TId, TValidator, TDto> : IRequestHandler<CreateEntityFromDto<T, TId, TDto>, T>
        where T : class, IHasId<TId>
        where TValidator : AbstractValidatorWrapper<T>
    {
        readonly TValidator Validator;
        readonly protected DbContext Context;
        readonly protected IMapper Mapper;

        public CreateEntityFromDtoHandler(DbContext dbContext, TValidator validator, IMapper mapper)
        {
            Context = dbContext;
            Mapper = mapper;
            Validator = validator;
        }

        public async Task<T> Handle(CreateEntityFromDto<T, TId, TDto> request, CancellationToken cancellationToken)
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
}
