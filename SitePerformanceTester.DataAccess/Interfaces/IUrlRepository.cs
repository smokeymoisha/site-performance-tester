using SitePerformanceTester.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.DataAccess.Interfaces
{
    public interface IUrlRepository
    {
        void Create(SitemapUrl url);
        IEnumerable<SitemapUrl> GetByRequestId(int id);
        IEnumerable<SitemapUrl> GetByUrl(string url);
    }
}
