using AutoMapper;
using SitePerformanceTester.BusinessLogic.Interfaces;
using SitePerformanceTester.BusinessLogic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SitePerformanceTester.BusinessLogic.Managers
{
    public class RequestManager : IRequestManager
    {
        private readonly IMapper _mapper;

        public RequestManager(IMapper mapper)
        {
            _mapper = mapper;
        }

        public void Create(SitemapRequestModel request)
        {
            throw new NotImplementedException();
        }

        public SitemapRequestModel GetByUrl(string url)
        {
            throw new NotImplementedException();
        }
    }
}
