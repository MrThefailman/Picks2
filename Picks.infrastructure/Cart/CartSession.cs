using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Picks.core.Entities;
using Picks.infrastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picks.infrastructure.Cart
{
    public class CartSession : Cart
    {
        [JsonIgnore] //No need to Deserialize or Serialize the ISession property
        public ISession Session { get; private set; }

        public static Cart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            CartSession cart = session.Get<CartSession>(CartKey) ?? new CartSession();
            cart.Session = session;
            return cart;
        }

        public override void AddToCart(Image img, int quantity)
        {
            base.AddToCart(img, quantity);
            CommitToSession();
        }

        public override void RemoveCartRow(Image p)
        {
            base.RemoveCartRow(p);
            CommitToSession();
        }

        public override void EmptyCart()
        {
            base.EmptyCart();
            Session.Remove(CartKey);
        }

        private void CommitToSession()
        {
            Session.Set(CartKey, this);
        }
    }
}
//await HttpContext.Session.LoadAsync();
//var value = await _cache.GetStringAsync("The_cache_key");

//if (value == null)
//{
//    value = $"value from session: {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
//    await _cache.SetStringAsync("The_cache_key", value);
//}

//ViewData["CacheData"] = $"Cached time: {value}";
//ViewData["CurrentTime"] = $"Current time {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";

//var theNameFromSession = HttpContext.Session.Get<string>("name");
//if (string.IsNullOrEmpty(theNameFromSession))
//{
//    HttpContext.Session.Set("name", name);
//    theNameFromSession = name;
//}

//ViewData["TheName"] = $"The name from the session: {theNameFromSession}";