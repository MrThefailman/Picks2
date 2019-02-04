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
            try
            {
                _categoryService.Add(vm);

                ViewData["Banner-class"] = "alert-success";
                ViewData["Banner-text"] = "Du har skapat en ny kategori :D";
            }
            catch
            {
                ViewData["Banner-class"] = "alert-danger";
                ViewData["Banner-text"] = "Något gick snett :(";
            }
            return RedirectToAction("Index", "Image");
        }
    }
}