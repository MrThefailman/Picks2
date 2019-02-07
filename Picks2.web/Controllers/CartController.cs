using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.core.Entities;
using Picks.infrastructure.Cart;
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
                _cart.AddToCart(img, 1);
            }

            return RedirectToAction(nameof(Index), new { returnUrl });
        }
    }
}