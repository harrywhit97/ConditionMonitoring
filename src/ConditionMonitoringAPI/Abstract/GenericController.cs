using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNet.OData;
using System;
using System.Text;
using Domain.Interfaces;
using System.Threading.Tasks;
using ConditionMonitoringAPI.Utils;
using FluentValidation.Results;
using MediatR;
using ConditionMonitoringAPI.Features.Crosscutting.Commands;
using System.Threading;
using ConditionMonitoringAPI.Features.Crosscutting.Queries;

namespace ConditionMonitoringAPI.Abstract
{
    [Route("api/[controller]")]
    public abstract class GenericController<T, TId, TValidator> : ControllerBase
        where T : class, IHaveId<TId> 
        where TValidator : AbstractValidatorWrapper<T>
    {
        readonly TValidator Validator;
        readonly DbContext Context;
        readonly DbSet<T> Repository;
        readonly IMediator Mediator;

        public GenericController(DbContext context, TValidator validator, IMediator mediator)
        {
            Context = context;
            Validator = validator;
            Mediator = mediator;
            Repository = Context.Set<T>();
        }

        [HttpGet("{Id}")]
        public virtual async Task<IActionResult> Get([FromQuery] TId Id)
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
        public virtual async Task<IActionResult> Post([FromBody] T entity)
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
        public virtual IActionResult Delete([FromODataUri] long key)
        {
            var entity = Repository.Find(key); ;

            if (entity is null)
                return NotFound();

            Repository.Remove(entity);
            Context.SaveChanges();
            return Ok();
        }

        [HttpPatch]
        public async Task<IActionResult> Patch([FromODataUri] TId key, [FromBody] Delta<T> entityDelta)
        {
            var entity = Repository.Find(key);

            if (entity is null)
                return NotFound();

            entityDelta.Patch(entity);

            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(key))
                    return NotFound();
                else
                    throw;
            }

            return Ok(entity);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromODataUri]TId key, [FromBody] T update)
        {
            try
            {
                update = SanatizeData.SanitizeStrings(update);
                ValidateEntity(update);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            if (!key.Equals(update.Id))
                return BadRequest();

            Context.Entry(update).State = EntityState.Modified;
            try
            {
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EntityExists(key))
                    return NotFound();
                else
                    throw;
            }
            return Ok(update);
        }

        bool EntityExists(TId key) => Repository.Any(x => x.Id.Equals(key));
        
        void ValidateEntity(T entity) 
        { 
            var result = Validator.Validate(entity);
            
            if (!result.IsValid)
                throw new ArgumentException(GetValidationErrorString(result));
        }

        string GetValidationErrorString(ValidationResult result)
        {
            var errors = new StringBuilder();
            result.Errors.Select(x => errors.Append($"{x.ErrorMessage};"));
            return errors.ToString();
        }
    }
}
