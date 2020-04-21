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
    public class UpdateEntityFromDto<T, TId, TDto> : IRequest<T>
        where T : class, IHasId<TId>
    {
        public TId Id { get; set; }
        public TDto Dto { get; set; }

        public UpdateEntityFromDto(TId id, TDto dto)
        {
            Id = id;
            Dto = dto;
        }
    }

    public abstract class UpdateEntityFromDtoHandler<T, TId, TValidator, TDto> : IRequestHandler<UpdateEntityFromDto<T, TId, TDto>, T>
        where T : class, IHasId<TId>
        where TValidator : AbstractValidatorWrapper<T>
    {
        readonly TValidator Validator;
        protected readonly DbContext Context;
        readonly IMapper Mapper;

        public UpdateEntityFromDtoHandler(DbContext dbContext, TValidator validator, IMapper mapper)
        {
            Validator = validator;
            Context = dbContext;
            Mapper = mapper;
        }

        public virtual async Task<T> Handle(UpdateEntityFromDto<T, TId, TDto> request, CancellationToken cancellationToken)
        {
            T entity;
            try
            {
                var e = await Context.Set<T>().FindAsync(request.Id)
                   ?? throw new NotFoundException(nameof(T), request.Id);

                Context.Entry(e).State = EntityState.Detached;

                request.Dto = SanatizeData.SanitizeStrings(request.Dto);

                entity = Mapper.Map<T>(request.Dto);
                entity.Id = request.Id;

                ValidationUtils.ValidateEntity(Validator, entity);

                Context.Entry(entity).State = EntityState.Modified;
                await Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new BadRequestException(e.Message);
            }
            return entity;
        }
    }
}
