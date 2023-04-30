using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iService_Admin.Tools
{
    internal class HttpConnectionServer
    {
        private HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }
        public HttpConnectionServer() { }

        public HttpClient GetHttpClient()
        {
            HttpClient _httpClient = new HttpClient(GetInsecureHandler())
            {
                //BaseAddress = new Uri("https://iservice-api.azurewebsites.net")
                BaseAddress = new Uri("https://82.77.118.177:81/")
            };
            return _httpClient;
        }
    }
}
