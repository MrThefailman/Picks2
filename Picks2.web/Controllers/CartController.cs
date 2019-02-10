using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.core.Entities;
using Picks.infrastructure.Cart;
using Picks.infrastructure.Constants;
using Picks.infrastructure.Extensions;
using Picks.infrastructure.Services.Interfaces;

namespace Picks.web.Controllers
{
    public class CartController : Controller
    {
        private readonly IImageService _imageService;
        private readonly CartSession _cart;
        public CartController(IImageService imageService, CartSession cart)
        {
            _imageService = imageService;
            _cart = cart;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<RedirectToActionResult> AddToCart(int imgId, string returnUrl)
        {
            if (imgId > 0)
            {
                var img = await _imageService.GetById(imgId);
                var cart = await _cart.AddToCartAsync(img, 1);

                var sessionCart = await HttpContext.Session.Get<string>(SessionKeys.CartKey);
                if (cart.Count() > 0)
                {
                    await HttpContext.Session.Set(SessionKeys.CartKey, cart);
                    var cartItems = cart;
                }
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }
    }
}