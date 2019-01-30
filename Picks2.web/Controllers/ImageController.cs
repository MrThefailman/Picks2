using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.infrastructure.Services.Interfaces;
using Picks.infrastructure.ViewModels;

namespace Picks.web.Controllers
{
    public class ImageController : Controller
    {
        private IImageService _imageService;
        private ICategoryService _categoryService;
        public ImageController(
            IImageService imageService,
            ICategoryService categoryService)
        {
            _imageService = imageService;
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(AddImageViewModel vm)
        {
            var newVM = new AddImageViewModel
            {
                Categories = await _categoryService.Get()
            };
            return View(newVM);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddImageViewModel vm)
        {
            await _imageService.Add(vm);

            return RedirectToAction("Index", "Home");
        }
    }
}