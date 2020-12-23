using Microsoft.EntityFrameworkCore;
using SitePerformanceTester.DataAccess.Models;
using System;

namespace SitePerformanceTester.DataAccess
{
    public class TesterContext : DbContext
    {
        public TesterContext(DbContextOptions options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<SitemapRequest> SitemapRequests { get; set; }
        public DbSet<SitemapUrl> SitemapUrls { get; set; }
    }
}
