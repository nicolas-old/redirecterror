using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using RedirectError.Models;

namespace RedirectError.Controllers
{
    public class HomeController : Controller
    {
        private IMemoryCache _memoryCache{get;set;}
        private const string CACHEKEY = "RedirectErr-Key";
        private const int CACHEDAYS = 2;

        public HomeController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public IActionResult Index()
        {
            var lastErrors = _memoryCache.Get(CACHEKEY) as IList<EventRequest>;
            ViewData["CacheDays"] = CACHEDAYS;
            return View(lastErrors);
        }

        public IActionResult RedirectErr([FromQuery] EventRequest evt)
        {
            IList<EventRequest> lastErrors;
            if(!_memoryCache.TryGetValue(CACHEKEY, out lastErrors))
            {
                lastErrors = new List<EventRequest>();
                lastErrors.Add(evt);
            }
            else
                lastErrors.Add(evt);
            _memoryCache.Set(CACHEKEY, 
                lastErrors, 
                new MemoryCacheEntryOptions().SetAbsoluteExpiration(
                    TimeSpan.FromDays(CACHEDAYS)
                    )
            );

            ViewData["LinkTo"] = string.Format("{0} {1} {2}", evt.EvtID, evt.EvtSrc, evt.ProdName);
            return View(evt); 
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            ViewData["URLRedirect"] = $"http://{Request.Headers["host"]}"; 
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
