using Domain.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace ConditionMonitoringAPI.Abstract
{
    public abstract class ReadOnlyController<TEntity, TId> : ODataController where TEntity : class, IHaveId<TId>
    {
        readonly protected DbSet<TEntity> Repository;
        readonly protected DbContext Context;

        public ReadOnlyController(DbContext context)
        {
            context.Database.EnsureCreated();
            Context = context;
            Repository = context.Set<TEntity>();
        }

        [EnableQuery]
        public virtual IEnumerable<TEntity> Get() => Repository.AsQueryable();

        [EnableQuery]
        public virtual ActionResult<TEntity> Get([FromODataUri] TId key)
        {
            var entity = Repository.Find(key);

            if (entity is null)
                return NotFound();
            return entity;
        }
    }
}

