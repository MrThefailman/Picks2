using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Picks.core.Entities;
using Picks.infrastructure.Extensions;
using Picks.infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Picks.infrastructure.Cart
{
    public class CartSession : Cart
    {
        [JsonIgnore] //No need to Deserialize or Serialize the ISession property
        public ISession Session { get; private set; }

        public static async Task<Cart> GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            CartSession cart = await session.Get<CartSession>(CartKey) ?? new CartSession();
            cart.Session = session;
            return cart;
        }

        public override async Task AddToCart(ImageViewModel img, int quantity)
        {
            await base.AddToCart(img, quantity);
            await CommitToSession();
        }

        public override async Task RemoveCartRow(ImageViewModel img)
        {
            await base.RemoveCartRow(img);
            await CommitToSession();
        }

        public override async Task EmptyCart()
        {
            await base.EmptyCart();
            Session.Remove(CartKey);
        }

        private async Task CommitToSession()
        {
            await Session.Set(CartKey, this);
        }
    }
}
