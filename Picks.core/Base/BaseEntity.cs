using System;

namespace Picks.core.Base
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
    }
}
