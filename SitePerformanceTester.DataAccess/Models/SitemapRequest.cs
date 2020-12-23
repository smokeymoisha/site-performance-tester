using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.DataAccess.Models
{
    public class SitemapRequest
    {
        public SitemapRequest()
        {
            SitemapUrls = new List<SitemapUrl>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string SitemapUrl { get; set; }
        public DateTime Date { get; set; }

        public IEnumerable<SitemapUrl> SitemapUrls { get; set; }
    }
}
