using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitePerformanceTester.MVC.Models
{
    public class SitemapRequestPostModel
    {
        public string Url { get; set; }
        public string SitemapUrl { get; set; }
        public DateTime Date { get; set; }
    }
}
