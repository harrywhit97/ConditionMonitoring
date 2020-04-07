using Domain.Interfaces;
using Microsoft.AspNet.OData;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ConditionMonitoringAPI.Abstract
{
    public abstract class OdataController<T, TId> : ODataController 
        where T : class, IHaveId<TId>
    {
        readonly protected DbContext Context;

        public OdataController(DbContext context)
        {
            context.Database.EnsureCreated();
            Context = context;
        }

        [EnableQuery]
        public virtual IEnumerable<T> Get() => Context.Set<T>().AsQueryable();
    }
}

