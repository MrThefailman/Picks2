using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Picks.core.Entities;
using Picks.infrastructure.Extensions;
using Picks.infrastructure.Services.Interfaces;
using Picks.infrastructure.ViewModels;

namespace Picks2.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;
        public HomeController(
            IDistributedCache cache,
            IImageService imageService,
            ICategoryService categoryService)
        {
            _cache = cache;
            _imageService = imageService;
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index(ImageCategoryViewModel vm)
        {
            IEnumerable<ImageViewModel> images = null;
            if (vm.CategoryId == 0)
            {
                images = await _imageService.Get();
            }
            else
            {
                images = await _imageService.GetByCategoryId(vm.CategoryId);
            }
            vm.Categories = await _categoryService.Get();
            vm.Images = images;
            
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(AddImageViewModel vm)
        {
            var UploadImage = Request.Form.Files;
            var result = await _imageService.UploadImage(UploadImage, vm);
            bool addedImage = false;
            if (result != null)
                addedImage = true;

            return RedirectToAction("Index", addedImage);
        }
    }
}
