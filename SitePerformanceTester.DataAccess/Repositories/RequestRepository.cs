using Microsoft.EntityFrameworkCore;
using SitePerformanceTester.DataAccess.Interfaces;
using SitePerformanceTester.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SitePerformanceTester.DataAccess.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly TesterContext _context;

        public RequestRepository(TesterContext context)
        {
            _context = context;
        }

        public void Create(SitemapRequest request)
        {
            _context.SitemapRequests.Add(request);
            _context.SaveChanges();

        }
        public IEnumerable<SitemapRequest> GetByUrl(string url)
        {
            var result = _context.SitemapRequests.Where(r => r.Url == url).Include(r => r.SitemapUrls);
            return result;
        }

        public SitemapRequest GetLatest()
        {
            int maxId = _context.SitemapRequests.Max(r => r.Id);

            var result = _context.SitemapRequests.FirstOrDefault(r => r.Id == maxId);
            return result;
        }
    }
}
