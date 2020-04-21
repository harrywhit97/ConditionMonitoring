using ConditionMonitoringAPI.Exceptions;
using Domain.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Crosscutting.Commands
{
    public class DeleteEntity<T, TId> : IRequest<bool>
        where T : class, IHasId<TId>
    {
        public TId Id { get; set; }

        public DeleteEntity(TId id)
        {
            Id = id;
        }
    }

    public abstract class DeleteEntityHandler<T, TId> : IRequestHandler<DeleteEntity<T, TId>, bool>
        where T : class, IHasId<TId>
    {
        readonly DbContext Context;

        public DeleteEntityHandler(DbContext dbContext)
        {
            Context = dbContext;
        }

        public async Task<bool> Handle(DeleteEntity<T, TId> request, CancellationToken cancellationToken)
        {
            var entity = await Context.Set<T>().FindAsync(request.Id);

            if (entity == null)
                throw new NotFoundException(nameof(T), request.Id);

            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
            return true;
        }
    }
}
