using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SitePerformanceTester.BusinessLogic.Interfaces;
using SitePerformanceTester.BusinessLogic.Models;
using SitePerformanceTester.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SitePerformanceTester.MVC.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : ControllerBase
    {
        private readonly IRequestManager _requestManager;
        private readonly IMapper _mapper;

        public RequestController(IRequestManager requestManager, IMapper mapper)
        {
            _requestManager = requestManager;
            _mapper = mapper;
        }

        // GET: api/<RequestController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RequestController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RequestController>
        [HttpPost]
        public void Post([FromBody] SitemapRequestPostModel requestPostModel)
        {
            if (!ModelState.IsValid)
            {
                return;
            }

            requestPostModel.Date = DateTime.Now;

            var requestModel = _mapper.Map<SitemapRequestModel>(requestPostModel);

            _requestManager.Create(requestModel);
        }

        // PUT api/<RequestController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RequestController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
