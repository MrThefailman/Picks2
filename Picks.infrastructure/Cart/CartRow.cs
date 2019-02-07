using Picks.infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.infrastructure.Cart
{
    public class CartRow
    {
        public ImageViewModel Image { get; set; }
        public int Quantity { get; set; }
    }
}
