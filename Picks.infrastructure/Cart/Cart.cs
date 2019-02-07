using Picks.core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Picks.infrastructure.Cart
{
    public class Cart
    {
        internal const string CartKey = "Customer_key";

        private List<CartRow> _cartRows = new List<CartRow>();

        public virtual IEnumerable<CartRow> CartRows => _cartRows;

        public virtual void AddToCart(Image img, int quantity)
        {
            var cartRow = _cartRows.Where(x => x.Image.Id == img.Id).FirstOrDefault();
            if (cartRow == null)
            {
                _cartRows.Add(new CartRow
                {
                    Image = img,
                    Quantity = quantity
                });
            }
            else
            {
                cartRow.Quantity += quantity;
            }
        }

        public virtual void RemoveCartRow(Image img)
        {
            _cartRows.RemoveAll(x => x.Image.Id == img.Id);
        }

        public virtual void EmptyCart()
        {
            _cartRows.Clear();
        }
    }
}
