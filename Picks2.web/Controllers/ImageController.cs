using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Picks.core.Entities;
using Picks.infrastructure.Helpers;
using Picks.infrastructure.Services.Interfaces;
using Picks.infrastructure.ViewModels;

namespace Picks.web.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService _imageService;
        private readonly ICategoryService _categoryService;
        private readonly UploadImageHelper _uploadImageHelper;
        public ImageController(
            IImageService imageService,
            ICategoryService categoryService,
            UploadImageHelper uploadImageHelper)
        {
            _imageService = imageService;
            _categoryService = categoryService;
            _uploadImageHelper = uploadImageHelper;
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
        public async Task<IActionResult> Upload(AddImageViewModel vm)
        {
            var UploadImage = Request.Form.Files;
            var result = await _imageService.UploadImage(UploadImage, vm);

            return RedirectToAction("Index", "Home");
        }
    }
}