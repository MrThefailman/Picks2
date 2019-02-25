using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.infrastructure.CartService;
using Picks.infrastructure.Services.Interfaces;

namespace Picks.web.Controllers
{
    public class CartController : Controller
    {
        private readonly IImageService _imageService;
        private readonly Cart _cart;
        public CartController(IImageService imageService, Cart cart)
        {
            _imageService = imageService;
            _cart = cart;
        }
        public IActionResult Index()
        {
            var vm = new CartViewModel()
            {
                Cart = _cart
            };

            return View(vm);
        }

        public async Task<IActionResult> AddToCart(int imgId)
        {
            if (imgId > 0)
            {
                var img = await _imageService.GetById(imgId);
                if (img != null)
                {
                    _cart.AddToCart(img);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> RemoveFromCart(int imgId)
        {
            if(imgId > 0)
            {
                var img = await _imageService.GetById(imgId);
                if(img != null)
                {
                    _cart.RemoveCartRow(img);
                }
            }
            return RedirectToAction("Index");
        }
    }
}