using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using Domain.Interfaces;
using System.Threading.Tasks;
using MediatR;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using System.Threading;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;
using ConditionMonitoringAPI.Exceptions;
using System.Net;
using Microsoft.AspNet.OData;
using System.Collections.Generic;
using System.Linq;

namespace ConditionMonitoringAPI.Abstract
{
    [Route("api/[controller]")]
    public abstract class GenericController<T, TId> : ControllerBase
        where T : class, IHaveId<TId>
    {
        readonly DbContext Context;
        readonly DbSet<T> Repository;
        readonly IMediator Mediator;

        public GenericController(DbContext context, IMediator mediator)
        {
            Context = context;
            Mediator = mediator;
            Repository = Context.Set<T>();
        }

        [HttpGet]
        [EnableQuery]
        public virtual IQueryable<T> Get() => Context.Set<T>().AsQueryable();

        [HttpGet("{Id}")]
        public virtual async Task<IActionResult> Get(TId Id)
        {
            T entity;

            try
            {
                entity = await Mediator.Send(new GetById<T, TId>(Id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(entity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(T entity)
        {
            try
            {
                return Ok(await Mediator.Send(new CreateEntity<T, TId>(entity), new CancellationToken()));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete(TId Id)
        {
            try
            {
                return Ok(await Mediator.Send(new DeleteEntity<T, TId>(Id), new CancellationToken()));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(TId Id, T update)
        {
            if (update is null)
                return BadRequest();

            try
            {
                return Ok(await Mediator.Send(new UpdateEntity<T, TId>(Id, update), new CancellationToken()));
            }
            catch(RestException e)
            {
                var inner = e.Exception;
                switch (e.StatusCode)
                {                    
                    case HttpStatusCode.NotFound:
                        return NotFound(inner.Message);
                    default:
                        return BadRequest(inner.Message);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
