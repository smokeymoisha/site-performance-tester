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

            var requestModel = _mapper.Map<SitemapRequestModel>(requestPostModel);
            _requestManager.Create(requestModel);

            //==============================

            var urlList = new List<string>();

            if (requestPostModel.SitemapUrl == null)
            {
                urlList = _requestManager.ParseUrlsFromHtml(requestPostModel.Url).ToList();
            }
            else
            {
                urlList = _requestManager.ParseUrlsFromSitemap(requestPostModel.SitemapUrl);

                if (urlList.Count == 0)
                {
                    urlList = _requestManager.ParseUrlsFromHtml(requestPostModel.Url).ToList();
                }
            }

            if (urlList.Count == 0)
            {
                ModelState.AddModelError("SitemapUrls", "Unable to parse URLs for this site. ");
                return View();
            }

            var urlViewModelList = new List<SitemapUrlViewModel>();

            foreach (string url in urlList)
            {
                var urlViewModel = new SitemapUrlViewModel();
                urlViewModel.Url = url;
                urlViewModel.ResponseTime = _sitemapUrlManager.MeasureResponseTime(url);
                urlViewModel.SitemapRequestId = _requestManager.GetLatest().Id;
                urlViewModel.MaxResponseTime = _sitemapUrlManager.GetMaxResponseTimeForUrl(url);

                if (urlViewModel.MaxResponseTime == null || urlViewModel.MaxResponseTime < urlViewModel.ResponseTime)
                {
                    urlViewModel.MaxResponseTime = urlViewModel.ResponseTime;
                }

                urlViewModel.MinResponseTime = _sitemapUrlManager.GetMinResponseTimeForUrl(url);

                if (urlViewModel.MinResponseTime == null || urlViewModel.MinResponseTime > urlViewModel.ResponseTime)
                {
                    urlViewModel.MinResponseTime = urlViewModel.ResponseTime;
                }

                var urlModel = _mapper.Map<SitemapUrlModel>(urlViewModel);
                _sitemapUrlManager.Create(urlModel);

                urlViewModelList.Add(urlViewModel);
            }

            var result = urlViewModelList.OrderByDescending(url => url.ResponseTime);

            return View("Result", result);
        }

        [HttpPost]
        public IActionResult History(SitemapRequestPostModel requestPostModel)
        {
            var list = _requestManager.GetByUrl(requestPostModel.Url);

            var resultList = new List<SitemapRequestPostModel>();

            foreach(var request in list)
            {
                var postModel = _mapper.Map<SitemapRequestPostModel>(request);
                resultList.Add(postModel);
            }

            return View(resultList);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
