using SitePerformanceTester.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.BusinessLogic.Interfaces
{
    public interface ISitemapUrlManager
    {
        void Create(SitemapUrlModel urlModel);
        long MeasureResponseTime(string url);
        long? GetMaxResponseTimeForUrl(string url);
        long? GetMinResponseTimeForUrl(string url);
    }
}
