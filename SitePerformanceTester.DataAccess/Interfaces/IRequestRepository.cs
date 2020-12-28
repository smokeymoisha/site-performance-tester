using SitePerformanceTester.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.DataAccess.Interfaces
{
    public interface IRequestRepository
    {
        void Create(SitemapRequest request);
        IEnumerable<SitemapRequest> GetByUrl(string url);
        SitemapRequest GetLatest();
    }
}
