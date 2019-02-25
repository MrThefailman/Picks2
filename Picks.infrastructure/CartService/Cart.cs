using Picks.infrastructure.Cart;
using Picks.infrastructure.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.infrastructure.CartService
{
    public class Cart
    {
        internal const string CartKey = "Customer_key";

        private List<CartRow> _cartRows = new List<CartRow>();

        public virtual void AddToCart(ImageViewModel img)
        {
            var cartRow = _cartRows.Where(x => x.Image.Id == img.Id).FirstOrDefault();
            if (cartRow == null)
            {
                _cartRows.Add(new CartRow
                {
                    Image = img
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

        public virtual IEnumerable<CartRow> CartRows => _cartRows;
    }
}
