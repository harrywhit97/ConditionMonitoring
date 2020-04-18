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
using Microsoft.Extensions.Logging;

namespace ConditionMonitoringAPI.Abstract
{
    [Route("api/[controller]")]
    public abstract class AbstractController<T, TId, TDto> : ControllerBase
        where T : class, IHasId<TId>
    {
        readonly DbContext Context;
        readonly IMediator Mediator;
        readonly ILogger Logger;

        public AbstractController(DbContext context, IMediator mediator, ILogger logger)
        {
            Context = context;
            Mediator = mediator;
            Logger = logger;
            Context.Database.EnsureCreated();
        }

        [HttpGet]
        [EnableQuery]
        public virtual IQueryable<T> Get() 
        {
            Logger.LogDebug("Recieved Get All request");
            return Mediator.Send(new GetEntiities<T, TId>()).Result;
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> Get(TId Id)
        {
            Logger.LogDebug("Recieved Get request");
            T entity;

            try
            {
                entity = await Mediator.Send(new GetEntityById<T, TId>(Id));
            }
            catch (Exception e)
            {
                Logger.LogError(e, "There was an error processing a Get request");
                return NotFound(e.Message);
            }
            return Ok(entity);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Post([FromBody] TDto dto)
        {
            Logger.LogDebug("Recieved Post request");
            if (dto is null)
                return BadRequest();
            try
            {
                var entity = await Mediator.Send(new CreateEntityFromDto<T, TId, TDto>(dto), new CancellationToken());
                return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
            }
            catch (RestException e)
            {
                Logger.LogError(e, "There was an error processing a Post request");
                if (e.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "There was an error processing a Post request");
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Delete(TId Id)
        {
            Logger.LogDebug("Recieved Delete request");
            try
            {
                await Mediator.Send(new DeleteEntity<T, TId>(Id), new CancellationToken());
                return Ok();
            }
            catch (RestException e)
            {
                Logger.LogError(e, "There was an error processing a Delete request");
                if (e.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "There was an error processing a Delete request");
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(TId Id, [FromBody] TDto update)
        {
            Logger.LogDebug("Recieved Put request");
            if (update is null)
                return BadRequest();

            try
            {
                return Ok(await Mediator.Send(new UpdateEntityFromDto<T, TId, TDto>(Id, update), new CancellationToken()));
            }
            catch(RestException e)
            {
                Logger.LogError(e, "There was an error processing a Put request");

                if (e.StatusCode == HttpStatusCode.NotFound)
                    return NotFound(e.Message);

                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "There was an error processing a Put request");
                return BadRequest(e.Message);
            }
        }
    }
}
