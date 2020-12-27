using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SitePerformanceTester.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SitePerformanceTester.DataAccess;
using SitePerformanceTester.BusinessLogic.Interfaces;
using AutoMapper;
using SitePerformanceTester.BusinessLogic.Models;

namespace SitePerformanceTester.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRequestManager _requestManager;
        private readonly ISitemapUrlManager _sitemapUrlManager;
        private readonly IMapper _mapper;

        public HomeController(IRequestManager requestManager, ISitemapUrlManager sitemapUrlManager,
            IMapper mapper)
        {
            _requestManager = requestManager;
            _sitemapUrlManager = sitemapUrlManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(SitemapRequestPostModel requestPostModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            requestPostModel.Date = DateTime.Now;
            requestPostModel.SitemapUrl = _requestManager.LocateSitemap(requestPostModel.Url);

            if (requestPostModel.SitemapUrl == null)
            {
                ModelState.AddModelError("SitemapUrl", "Unable to locate sitemap for this URL.");
                return View();
            }

            //var test = _requestManager.ParseUrlsFromSitemap(requestPostModel.SitemapUrl);

            //long test = _sitemapUrlManager.MeasureResponseTime(requestPostModel.Url);

            var requestModel = _mapper.Map<SitemapRequestModel>(requestPostModel);

            _requestManager.Create(requestModel);

            return View("Result");
        }

        public IActionResult Result()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
