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
        private ICategoryService _categoryService;
        public ImageController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var vm = new AddImageViewModel
            {
                Categories = await _categoryService.Get()
            };
            return View(vm);
        }
    }
}