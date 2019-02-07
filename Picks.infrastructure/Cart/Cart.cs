using Picks.core.Entities;
using Picks.infrastructure.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Picks.infrastructure.Cart
{
    public class Cart
    {
        internal const string CartKey = "Customer_key";

        private List<CartRow> _cartRows = new List<CartRow>();

        public virtual IEnumerable<CartRow> CartRows => _cartRows;

        public virtual void AddToCart(ImageViewModel img, int quantity)
        {
            var cartRow = _cartRows.Where(x => x.Image == img).FirstOrDefault();
            if (cartRow == null)
            {
                _cartRows.Add(new CartRow
                {
                    Image = img,
                    Quantity = quantity
                });
            }
        }

        public virtual void RemoveCartRow(ImageViewModel img)
        {
            _cartRows.RemoveAll(x => x.Image == img);
        }

        public virtual void EmptyCart()
        {
            _cartRows.Clear();
        }
    }
}
