using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace SitePerformanceTester.BusinessLogic
{
    public static class UrlMethods
    {
        public static bool UrlIsValid(string url)
        {
            try
            {
                var request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Timeout = 5000;
                request.Method = "HEAD";

                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    int statusCode = (int)response.StatusCode;
                    if (statusCode >= 100 && statusCode < 400)
                    {
                        return true;
                    }
                    else if (statusCode >= 500 && statusCode <= 510)
                    {
                        return false;
                    }
                }
            }
            catch (WebException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static string ReadRobots(string url)
        {
            string result = null;

            var request = HttpWebRequest.Create(url) as HttpWebRequest;
            var response = request.GetResponse() as HttpWebResponse;

            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = "";
                    string sitemapLine = "Sitemap: ";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith(sitemapLine))
                        {
                            result = line.Substring(sitemapLine.Length);
                        }
                    }
                }
            }
            response.Close();

            return result;
        }
    }
}
