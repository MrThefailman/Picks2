﻿using Picks.core.Base;
using System.Collections.Generic;

namespace Picks.core.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
