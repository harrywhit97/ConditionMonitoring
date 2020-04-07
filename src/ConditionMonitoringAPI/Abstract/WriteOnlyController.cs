using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FluentValidation;
using Microsoft.AspNet.OData;
using System;
using System.Text;
using Domain.Interfaces;
using System.Threading.Tasks;
using ConditionMonitoringAPI.Utils;
using FluentValidation.Results;

namespace ConditionMonitoringAPI.Abstract
{
    [Route("api/[controller]")]
    public abstract class WriteOnlyController<TEntity, TId, TValidator> : ControllerBase
        where TEntity : class, IHaveId<TId> 
        where TValidator : AbstractValidator<TEntity>
    {
        readonly TValidator Validator;
        readonly DbContext Context;
        readonly DbSet<TEntity> Repository;

        public WriteOnlyController(DbContext context, TValidator validator)
        {
            Context = context;
            Validator = validator;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] TEntity entity)
        {
            try
            {
                entity = SanatizeData.SanitizeStrings(entity);
                ValidateEntity(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

            Repository.Add(entity);
            await Context.SaveChangesAsync();
            return Ok(entity);
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

            return Ok(entity);
        }

        [HttpPut]
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
            return Ok(update);
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
