using SitePerformanceTester.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.BusinessLogic.Interfaces
{
    public interface IRequestManager
    {
        void Create(SitemapRequestModel request);
        SitemapRequestModel GetByUrl(string url);
        string LocateSitemap(string url);
    }
}
