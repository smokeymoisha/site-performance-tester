using AutoMapper;
using SitePerformanceTester.BusinessLogic.Interfaces;
using SitePerformanceTester.BusinessLogic.Models;
using SitePerformanceTester.DataAccess.Interfaces;
using SitePerformanceTester.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.NetworkInformation;
using System.Net;
using System.Diagnostics;

namespace SitePerformanceTester.BusinessLogic.Managers
{
    public class SitemapUrlManager : ISitemapUrlManager
    {
        private readonly IUrlRepository _urlRepository;
        private readonly IMapper _mapper;

        public SitemapUrlManager(IUrlRepository urlRepository, IMapper mapper)
        {
            _urlRepository = urlRepository;
            _mapper = mapper;
        }

        public void Create(SitemapUrlModel urlModel)
        {
            var url = _mapper.Map<SitemapUrl>(urlModel);
            _urlRepository.Create(url);
        }

        public long MeasureResponseTime(string url)
        {
            long result = -1;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            var timer = new Stopwatch();
            timer.Start();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            timer.Stop();

            result = timer.ElapsedMilliseconds;

            return result;
        }
    }
}
