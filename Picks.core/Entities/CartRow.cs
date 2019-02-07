using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.core.Entities
{
    public class CartRow
    {
        public virtual Image Image { get; set; }
        public int Quantity { get; set; }
    }
}
