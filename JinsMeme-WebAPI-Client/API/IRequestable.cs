using System.Net.Http;

namespace JinsMeme_WebAPI_Client.API
{
    public interface IRequestable
    {
        HttpRequestMessage ToHttpRequestMessage();
    }
}
