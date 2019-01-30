using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Picks.infrastructure.Services.Interfaces;
using Picks.infrastructure.ViewModels;

namespace Picks.web.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public IActionResult Add(CategoryViewModel vm)
        {
            var newVM = new AddImageViewModel
            {
                BannerClass = "success",
                BannerText = "Du har lagt till en kategori"
            };
            try
            {
                _categoryService.Add(vm);
            }
            catch
            {
                newVM.BannerClass = "danger";
                newVM.BannerText = "Något gick snett :(";
            }
            return RedirectToAction("Index", "Image", newVM);
        }
    }
}