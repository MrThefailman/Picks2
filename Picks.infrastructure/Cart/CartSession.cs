﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Picks.core.Entities;
using Picks.infrastructure.Extensions;
using Picks.infrastructure.ViewModels;
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

        public override void AddToCart(ImageViewModel img, int quantity)
        {
            base.AddToCart(img, quantity);
            CommitToSession();
        }

        public override void RemoveCartRow(ImageViewModel img)
        {
            base.RemoveCartRow(img);
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
