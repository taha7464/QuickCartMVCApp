using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System;
using System.Configuration;

namespace QuickKartMVCApp.Repository
{
    public class ServiceRepository
    {
        private HttpClient Client { get; set; }

        public ServiceRepository()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"].ToString());
        }

        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }
    }
}