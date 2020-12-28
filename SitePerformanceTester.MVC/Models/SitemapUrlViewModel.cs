using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SitePerformanceTester.MVC.Models
{
    public class SitemapUrlViewModel
    {
        public string Url { get; set; }
        public long ResponseTime { get; set; }
        public long MaxResponseTime { get; set; }
        public long MinResponseTime { get; set; }
        public int SitemapRequestId { get; set; }
    }
}
