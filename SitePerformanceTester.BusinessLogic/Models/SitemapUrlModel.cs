using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.BusinessLogic.Models
{
    public class SitemapUrlModel
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public long ResponseTime { get; set; }
        public int SitemapRequestId { get; set; }

        public SitemapRequestModel SitemapRequest { get; set; }

    }
}
