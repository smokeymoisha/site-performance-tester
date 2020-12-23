using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SitePerformanceTester.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SitePerformanceTester.DataAccess;

namespace SitePerformanceTester.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var ping = new System.Net.NetworkInformation.Ping();

            //var result = ping.Send("www.google.com");

            //if (result.Status == System.Net.NetworkInformation.IPStatus.Success)

            return View();
        }

        public IActionResult Privacy()
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
