using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.DataAccess.Models
{
    public class SitemapUrl
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public long ResponseTime { get; set; }
        public int SitemapRequestId { get; set; }

        public SitemapRequest SitemapRequest { get; set; }

    }
}
