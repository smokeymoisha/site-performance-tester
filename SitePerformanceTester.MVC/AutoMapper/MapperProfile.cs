using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SitePerformanceTester.BusinessLogic.Models;
using SitePerformanceTester.DataAccess.Models;
using SitePerformanceTester.MVC.Models;

namespace SitePerformanceTester.MVC.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<SitemapRequest, SitemapRequestModel>();
            CreateMap<SitemapRequestModel, SitemapRequest>();
            CreateMap<SitemapRequestPostModel, SitemapRequestModel>();

            CreateMap<SitemapUrl, SitemapUrlModel>();
            CreateMap<SitemapUrlModel, SitemapUrl>();
            CreateMap<SitemapUrlModel, SitemapUrlViewModel>(); 
        }
    }
}
