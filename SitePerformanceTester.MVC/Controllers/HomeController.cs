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
        private readonly ILogger<HomeController> _logger;
        private readonly IRequestManager _requestManager;
        private readonly IMapper _mapper;

        public HomeController(ILogger<HomeController> logger, IRequestManager requestManager,
            IMapper mapper)
        {
            _logger = logger;
            _requestManager = requestManager;
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

            var requestModel = _mapper.Map<SitemapRequestModel>(requestPostModel);

            _requestManager.Create(requestModel);

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
