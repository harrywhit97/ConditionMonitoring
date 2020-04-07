using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using Domain.Interfaces;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Abstract
{
    public abstract class ReadOnlyController<T, TId> : ODataController 
        where T : class, IHaveId<TId>
    {
        readonly protected DbContext Context;
        readonly protected IMediator Mediator;
        readonly protected DbSet<T> Repository;

        public ReadOnlyController(DbContext context, IMediator mediator)
        {
            context.Database.EnsureCreated();
            Context = context;
            Repository = Context.Set<T>();
            Mediator = mediator;
        }

        [EnableQuery]
        public virtual IEnumerable<T> Get() => Context.Set<T>().AsQueryable();

        [EnableQuery]
        public async virtual Task<ActionResult<T>> Get([FromODataUri] TId key)
        {
            T entity;

            try
            {
                entity = await Mediator.Send(new GetById<T, TId>(key));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
            return entity;
        }
    }
}

