using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.BusinessLogic.Models
{
    public class SitemapRequestModel
    {
        public SitemapRequestModel()
        {
            SitemapUrls = new List<SitemapUrlModel>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string SitemapUrl { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<SitemapUrlModel> SitemapUrls { get; set; }
    }
}
