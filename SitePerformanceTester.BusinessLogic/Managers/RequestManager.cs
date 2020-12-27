using AutoMapper;
using SitePerformanceTester.BusinessLogic;
using SitePerformanceTester.BusinessLogic.Interfaces;
using SitePerformanceTester.BusinessLogic.Models;
using SitePerformanceTester.DataAccess.Interfaces;
using SitePerformanceTester.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;

namespace SitePerformanceTester.BusinessLogic.Managers
{
    public class RequestManager : IRequestManager
    {
        private readonly IMapper _mapper;
        private readonly IRequestRepository _repository;

        public RequestManager(IMapper mapper, IRequestRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public void Create(SitemapRequestModel requestModel)
        {
            var request = _mapper.Map<SitemapRequest>(requestModel);
            _repository.Create(request);
        }

        public SitemapRequestModel GetByUrl(string url)
        {
            var request = _repository.GetByUrl(url);
            var requestModel = _mapper.Map<SitemapRequestModel>(request);

            return requestModel;
        }

        //public void PingSitemap(string url)
        //{
        //    var ping = new System.Net.NetworkInformation.Ping();

        //    var result = ping.Send(url);

        //    if (result.Status == System.Net.NetworkInformation.IPStatus.)
        //    {

        //    }
        //}

        public string LocateSitemap(string url)
        {
            string sitemapUrl = url.EndsWith('/') ? url + "sitemap.xml" : url + "/sitemap.xml";

            if (UrlMethods.UrlIsValid(sitemapUrl))
            {
                return sitemapUrl;
            }

            string robotsUrl = url.EndsWith('/') ? url + "robots.txt" : url + "/robots.txt";

            if (UrlMethods.UrlIsValid(robotsUrl))
            {
                string resultUrl = UrlMethods.ReadRobots(robotsUrl);
                return resultUrl;
            }

            return string.Empty;
        }
    }
}
