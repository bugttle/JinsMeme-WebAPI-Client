using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace JinsMeme_WebAPI_Client.Http
{
    public class Client
    {
        static HttpClient httpClient = new HttpClient();

        /**
         * Launch browser with an URL
         */
        public void OpenBrowser(Browser.IRequestable request)
        {
            var s = request.ToURL();
            Process.Start(request.ToURL());
        }

        public async Task<T> SendRequestAsObjectAsync<T>(API.IRequestable request) where T : API.IResponsable
        {
            var response = await httpClient.SendAsync(request.ToHttpRequestMessage());
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }

        public async Task<string> SendRequestAsStringAsync(API.IRequestable request)
        {
            var response = await httpClient.SendAsync(request.ToHttpRequestMessage());
            var json = await response.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject(json);
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}
