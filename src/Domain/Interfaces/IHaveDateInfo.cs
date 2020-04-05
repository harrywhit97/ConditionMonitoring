using System;

namespace Domain.Interfaces
{
    public interface IHaveDateInfo
    {
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
    }
}
