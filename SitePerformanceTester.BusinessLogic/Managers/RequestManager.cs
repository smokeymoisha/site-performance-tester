using AutoMapper;
using SitePerformanceTester.BusinessLogic.Interfaces;
using SitePerformanceTester.BusinessLogic.Models;
using SitePerformanceTester.DataAccess.Interfaces;
using SitePerformanceTester.DataAccess.Models;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using HtmlAgilityPack;
using System;

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
            var result = new List<string>();

            var request = HttpWebRequest.Create(sitemapUrl) as HttpWebRequest;
            var response = request.GetResponse() as HttpWebResponse;

            if (response.ContentType.StartsWith("text/xml") || response.ContentType.StartsWith("application/xml"))
            {
                result = UrlMethods.ReadSitemapXml(sitemapUrl);
            }
            else if (response.ContentType.StartsWith("text/plain"))
            {
                result = UrlMethods.ReadSitemapTxt(sitemapUrl);
            }

            return result;
        }

        public IEnumerable<string> ParseUrlsFromHtml(string urlRoot)
        {
            var uriRoot = new Uri(urlRoot);

            var queue = new Queue<string>();
            var allUrls = new HashSet<string>();

            queue.Enqueue(urlRoot);
            allUrls.Add(urlRoot);

            while (queue.Count > 0)
            {
                string currentUrl = queue.Dequeue();

                yield return currentUrl;

                var web = new HtmlWeb();
                var doc = new HtmlDocument();
                doc = web.Load(urlRoot);

                var nodes = doc.DocumentNode.SelectNodes("//a[@href]");

                if (nodes == null) continue;

                foreach (var link in nodes)
                {
                    var att = link.Attributes["href"];
                    var href = att.Value;

                    Uri uri = new Uri(href, UriKind.RelativeOrAbsolute);

                    if (!uri.IsAbsoluteUri)
                    {
                        uri = new Uri(uriRoot, uri);
                    }

                    string uriString = uri.ToString();

                    if (!allUrls.Contains(uriString))
                    {
                        allUrls.Add(uriString);

                        if (uriRoot.IsBaseOf(uri))
                        {
                            queue.Enqueue(uriString);
                        }
                    }
                }
            }
        }

        public SitemapRequestModel GetLatest()
        {
            var request = _repository.GetLatest();

            var result = _mapper.Map<SitemapRequestModel>(request);
            return result;
        }
    }
}
