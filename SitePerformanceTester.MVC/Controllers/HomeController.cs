﻿using Microsoft.AspNetCore.Mvc;
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

            var requestModel = _mapper.Map<SitemapRequestModel>(requestPostModel);
            _requestManager.Create(requestModel);

            //==============================

            var urlList = _requestManager.ParseUrlsFromSitemap(requestPostModel.SitemapUrl);
            var urlViewModelList = new List<SitemapUrlViewModel>();

            foreach (string url in urlList)
            {
                var urlViewModel = new SitemapUrlViewModel();
                urlViewModel.Url = url;
                urlViewModel.ResponseTime = _sitemapUrlManager.MeasureResponseTime(url);
                urlViewModel.SitemapRequestId = _requestManager.GetLatest().Id;
                urlViewModel.MaxResponseTime = _sitemapUrlManager.GetMaxResponseTimeForUrl(url);
                urlViewModel.MinResponseTime = _sitemapUrlManager.GetMinResponseTimeForUrl(url);

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
