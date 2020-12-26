using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SitePerformanceTester.MVC.Models
{
    public class SitemapRequestPostModel
    {
        [Url( ErrorMessage = "Please enter a valid URL.")]
        [Required( ErrorMessage = "Please enter a URL.")]
        public string Url { get; set; }
        public string SitemapUrl { get; set; }
        public DateTime Date { get; set; }
    }
}
