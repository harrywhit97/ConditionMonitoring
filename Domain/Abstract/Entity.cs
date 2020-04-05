using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Abstract
{
    public abstract class Entity<TId> : IHaveId<TId>
    {
        public TId Id { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
    }
}
