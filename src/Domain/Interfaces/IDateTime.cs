using System;

namespace Domain.Interfaces
{
    public interface IDateTime
    {
        DateTimeOffset Now { get; }
    }
}
