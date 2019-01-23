using System;

namespace Picks.core
{
    public interface IEntity
    {
        int Id { get; set; }
        DateTime Created { get; set; }
    }
}
