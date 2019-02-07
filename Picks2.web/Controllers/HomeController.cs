using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Picks.infrastructure.Constants;
using Picks.infrastructure.Extensions;
using Picks.infrastructure.Services.Interfaces;
using Picks.infrastructure.ViewModels;

namespace Picks2.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        private readonly IImageService _imageService;
        public HomeController(
            IDistributedCache cache,
            IImageService imageService)
        {
            _cache = cache;
            _imageService = imageService;
        }
        public async Task<IActionResult> Index(int categoryId)
        {
            IEnumerable<ImageViewModel> images = null;
            if(categoryId == 0)
            {
                images = await _imageService.Get();
            }
            else
            {
                images = await _imageService.GetByCategoryId(categoryId);
            }

            return View(images);
        }
    }
}

//await HttpContext.Session.LoadAsync();
//var value = await _cache.GetStringAsync("The_cache_key");

//if (value == null)
//{
//    value = $"value from session: {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
//    await _cache.SetStringAsync("The_cache_key", value);
//}

//ViewData["CacheData"] = $"Cached time: {value}";
//ViewData["CurrentTime"] = $"Current time {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";

//var theNameFromSession = HttpContext.Session.Get<string>("name");
//if (string.IsNullOrEmpty(theNameFromSession))
//{
//    HttpContext.Session.Set("name", name);
//    theNameFromSession = name;
//}

//ViewData["TheName"] = $"The name from the session: {theNameFromSession}";