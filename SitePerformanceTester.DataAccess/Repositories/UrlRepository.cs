using SitePerformanceTester.DataAccess.Interfaces;
using SitePerformanceTester.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitePerformanceTester.DataAccess.Repositories
{
    public class UrlRepository : IUrlRepository
    {
        private readonly TesterContext _context;

        public UrlRepository(TesterContext context)
        {
            _context = context;
        }

        public void Create(SitemapUrl url)
        {
            _context.SitemapUrls.Add(url);
            _context.SaveChanges();
        }

        public IEnumerable<SitemapUrl> GetByRequestId(int id)
        {
            var result = _context.SitemapUrls.Where(u => u.SitemapRequestId == id);
            return result;
        }
    }
}
