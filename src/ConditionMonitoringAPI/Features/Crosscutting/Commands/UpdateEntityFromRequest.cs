using AutoMapper;
using ConditionMonitoringAPI.Exceptions;
using ConditionMonitoringAPI.Interfaces;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{
    public interface IUpdateEntityFromRequest<T, TId> : IRequest<T>, IHasId<TId>, IMapToo<T>
        where T : class, IHasId<TId>
    {
    }

    public abstract class UpdateEntityFromRequestHandler<T, TId, TRequest> : IRequestHandler<TRequest, T>
        where T : class, IHasId<TId>
        where TRequest : IRequest<T>, IMapToo<T>, IHasId<TId>
    {
        readonly protected DbContext _context;
        readonly protected IMapper _mapper;

        public UpdateEntityFromRequestHandler(DbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public async Task<T> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var existingEntity = await _context.Set<T>().FindAsync(request.Id)
                ?? throw new NotFoundException(typeof(T).Name, request.Id);

            _context.Entry(existingEntity).State = EntityState.Detached;

            var entity = _mapper.Map<T>(request);

            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
