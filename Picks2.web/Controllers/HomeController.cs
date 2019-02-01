using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Picks.infrastructure.Constants;

namespace Picks2.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistributedCache _cache;
        public HomeController(
            IDistributedCache cache)
        {
            _cache = cache;
        }
        public IActionResult Index(string name)
        {
            //await HttpContext.Session.LoadAsync();
            ////var value = _cache.GetValue<string>

            //if (value == null)
            //{
            //    value = $"value from session: {DateTime.Now.ToString(CultureInfo.CurrentCulture)}";
            //    HttpContext.Session.SetString(SessionKeys.SessionKey, value);
            //    await HttpContext.Session.CommitAsync();
            //}

            //ViewData["SessionMessage"] = $"Session value: {value}";
            return View();
        }
    }
}
