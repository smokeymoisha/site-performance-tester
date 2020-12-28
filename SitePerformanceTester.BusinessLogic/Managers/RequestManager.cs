﻿using AutoMapper;
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
using System.Xml;

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

        public List<SitemapRequestModel> GetByUrl(string url)
        {
            var requestList = _repository.GetByUrl(url);
            var resultList = new List<SitemapRequestModel>();

            foreach(var request in requestList)
            {
                var requestModel =_mapper.Map<SitemapRequestModel>(request);
                resultList.Add(requestModel);
            }

            return resultList;
        }

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

            return null;
        }

        public List<string> ParseUrlsFromSitemap(string sitemapUrl)
        {
            var request = HttpWebRequest.Create(sitemapUrl) as HttpWebRequest;
            var response = request.GetResponse() as HttpWebResponse;

            if (response.ContentType.StartsWith("text/xml") || response.ContentType.StartsWith("application/xml"))
            {
                var sitemapLinks = UrlMethods.ReadSitemapXml(sitemapUrl);

                return sitemapLinks;
            }
            else if (response.ContentType.StartsWith("text/plain"))
            {
                var sitemapLinks = UrlMethods.ReadSitemapTxt(sitemapUrl);

                return sitemapLinks;
            }

            return null;
        }

        public SitemapRequestModel GetLatest()
        {
            var request = _repository.GetLatest();

            var result = _mapper.Map<SitemapRequestModel>(request);
            return result;
        }
    }
}
