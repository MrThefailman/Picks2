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
        private readonly ICategoryService _categoryService;
        public ImageController(
            ICategoryService categoryService)
        {
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
    }
}