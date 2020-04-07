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

namespace ConditionMonitoringAPI.Abstract
{
    public abstract class GenericController<TEntity, TId, TValidator> : ReadOnlyController<TEntity, TId>
        where TEntity : class, IHaveId<TId> 
        where TValidator : AbstractValidatorWrapper<TEntity>
    {
        readonly TValidator Validator;

        public GenericController(DbContext context, TValidator validator, IMediator mediator) 
            : base(context, mediator)
        {
            Validator = validator;
        }

        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            try
            {
                return Created(await Mediator.Send(new CreateEntity<TEntity, TId>(entity), new CancellationToken()));
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        public virtual IActionResult Delete([FromODataUri] long key)
        {
            var entity = Repository.Find(key); ;

            if (entity is null)
                return NotFound();

            Repository.Remove(entity);
            Context.SaveChanges();
            return Ok();
        }

        public async Task<IActionResult> Patch([FromODataUri] TId key, [FromBody] Delta<TEntity> entityDelta)
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

            return Updated(entity);
        }

        public async Task<IActionResult> Put([FromODataUri]TId key, [FromBody] TEntity update)
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
            return Updated(update);
        }

        bool EntityExists(TId key) => Repository.Any(x => x.Id.Equals(key));
        
        void ValidateEntity(TEntity entity) 
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
