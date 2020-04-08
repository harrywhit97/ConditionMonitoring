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
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Net.Mime;

namespace ConditionMonitoringAPI.Abstract
{
    [Route("api/[controller]")]
    public abstract class GenericController<T, TId, TDto> : ControllerBase
        where T : class, IHasId<TId>
    {
        readonly DbContext Context;
        readonly IMediator Mediator;

        public GenericController(DbContext context, IMediator mediator)
        {
            Context = context;
            Mediator = mediator;

            Context.Database.EnsureCreated();
        }

        [HttpGet]
        [EnableQuery]
        public virtual IQueryable<T> Get() => Context.Set<T>().AsQueryable();

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> Get(TId Id)
        {
            T entity;

            try
            {
                entity = await Mediator.Send(new GetEntityById<T, TId>(Id));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Post([FromBody] TDto dto)
        {
            if (dto is null)
                return BadRequest();
            try
            {
                var entity = await Mediator.Send(new CreateEntityFromDto<T, TId, TDto>(dto), new CancellationToken());
                
                return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Delete(TId Id)
        {
            try
            {
                await Mediator.Send(new DeleteEntity<T, TId>(Id), new CancellationToken());
                return Ok();
            }
            catch (RestException e)
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(TId Id, [FromBody] TDto update)
        {
            if (update is null)
                return BadRequest();

            try
            {
                return Ok(await Mediator.Send(new UpdateEntityFromDto<T, TId, TDto>(Id, update), new CancellationToken()));
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
