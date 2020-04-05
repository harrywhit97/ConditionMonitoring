using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using FluentValidation;
using Microsoft.AspNet.OData;
using System.Text.RegularExpressions;
using System;
using System.Text;
using Domain.Interfaces;

namespace ConditionMonitoringAPI.Abstract
{
    public abstract class GenericController<TEntity, TId, TValidator> : ReadOnlyController<TEntity, TId> 
        where TEntity : class, IHaveId<TId> 
        where TValidator : AbstractValidator<TEntity>
    {
        readonly TValidator Validator;

        public GenericController(DbContext context, TValidator validator) : base(context)
        {
            Validator = validator;
        }

        public virtual IActionResult Post([FromBody] TEntity entity)
        {
            try
            {
                entity = Sanitize(entity);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

           // entity = SetCreatedAndUpdatedTimes(entity, DateTimeOffset.Now);

            Repository.Add(entity);
            Context.SaveChanges();
            return Created(entity);
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

        public IActionResult Patch([FromODataUri] TId key, [FromBody] Delta<TEntity> entityDelta)
        {
            var entity = Repository.Find(key);

            if (entity is null)
                return NotFound();

            //entity.DateUpdated = DateTimeOffset.Now;

            entityDelta.Patch(entity);

            try
            {
                Context.SaveChanges();
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

        public IActionResult Put([FromODataUri]TId key, [FromBody] TEntity update)
        {
            try
            {
                update = Sanitize(update);
               // update.DateUpdated = DateTimeOffset.Now;
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
                Context.SaveChanges();
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

        TEntity Sanitize(TEntity entity)
        {
            _ = entity ?? throw new ArgumentNullException();

            var t = entity.GetType();
            var properties = t.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var p = t.GetProperty(properties[i].Name);

                if (p.PropertyType == typeof(string))
                {
                    var value = p.GetValue(entity).ToString().Trim();
                    if (Regex.IsMatch(value, @"^[a-zA-Z0-9]+$"))
                        p.SetValue(entity, value);
                    else
                        throw new Exception($"Property '{p.Name}' can must only contain letters and numbers. '{value}' is not a valid value.");
                }
            }

            var result = Validator.Validate(entity);

            if (!result.IsValid)
            {
                var errors = new StringBuilder();
                result.Errors.Select(x => errors.Append($"{x.ErrorMessage};"));
                throw new ArgumentException(errors.ToString());
            }
            return entity;
        }

        //public TEntity SetCreatedAndUpdatedTimes(TEntity entity, DateTimeOffset now)
        //{
        //    entity.DateCreated = now;
        //    entity.DateUpdated = now;
        //    return entity;
        //}
    }
}
