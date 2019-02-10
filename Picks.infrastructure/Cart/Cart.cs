using Microsoft.AspNetCore;
using Picks.core.Entities;
using Picks.infrastructure.Constants;
using Picks.infrastructure.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picks.infrastructure.Cart
{
    public class Cart
    {
        internal const string CartKey = "Customer_key";

        private List<CartRow> _cartRows = new List<CartRow>();

        public virtual IEnumerable<CartRow> CartRows => _cartRows;

        public virtual async Task AddToCart(ImageViewModel img, int quantity)
        {
            var cartRow = _cartRows.Where(x => x.Image == img).FirstOrDefault();
            if (cartRow == null)
            {
                _cartRows.Add(new CartRow
                {
                    Image = img,
                    Quantity = quantity
                });

                if(_cartRows.Count() > 0)
                {

                }
            }
            
        }
        public virtual async Task<List<CartRow>> AddToCartAsync(ImageViewModel img, int quantity)
        {
            var cartRow = _cartRows.Where(x => x.Image == img).FirstOrDefault();
            if(cartRow == null)
            {
                _cartRows.Add(new CartRow
                {
                    Image = img,
                    Quantity = quantity
                });
            }
            return _cartRows;
        }

        public virtual async Task RemoveCartRow(ImageViewModel img)
        {
            _cartRows.RemoveAll(x => x.Image == img);
        }

        public virtual async Task EmptyCart()
        {
            _cartRows.Clear();
        }
    }
}
